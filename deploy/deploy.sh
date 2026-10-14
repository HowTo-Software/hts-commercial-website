#!/usr/bin/env bash
# Runs on the web host. Switches the stack to an image tag that is already loaded,
# health-checks it and rolls back to the previous tag on failure.
# Usage: HOST_PORT=5180 ./deploy.sh <image-tag>
set -euo pipefail

NEW_TAG="${1:?usage: deploy.sh <image-tag>}"
if [[ ! "$NEW_TAG" =~ ^[A-Za-z0-9_.-]{1,128}$ ]]; then
  echo "Invalid image tag: $NEW_TAG" >&2
  exit 2
fi

cd "$(dirname "$(readlink -f "$0")")"

IMAGE="hts-commercial-website"
HOST_PORT="${HOST_PORT:-5180}"
KEEP_IMAGES="${KEEP_IMAGES:-5}"

current_tag=""
if [[ -f .env ]]; then
  current_tag="$(sed -n 's/^IMAGE_TAG=//p' .env)"
fi

write_env() {
  printf 'IMAGE_TAG=%s\nHOST_PORT=%s\n' "$1" "$HOST_PORT" > .env.tmp
  mv .env.tmp .env
}

wait_healthy() {
  for _ in $(seq 1 30); do
    if curl -fs -o /dev/null --max-time 5 "http://127.0.0.1:${HOST_PORT}/"; then
      return 0
    fi
    sleep 2
  done
  return 1
}

if ! docker image inspect "${IMAGE}:${NEW_TAG}" > /dev/null 2>&1; then
  echo "Image ${IMAGE}:${NEW_TAG} is not loaded on this host." >&2
  exit 1
fi

echo "Deploying ${IMAGE}:${NEW_TAG} (previous: ${current_tag:-none}) on port ${HOST_PORT}"
write_env "$NEW_TAG"
docker compose up -d --remove-orphans

if wait_healthy; then
  if [[ -n "$current_tag" && "$current_tag" != "$NEW_TAG" ]]; then
    echo "$current_tag" > .previous-tag
  fi
  echo "Deployment healthy: ${IMAGE}:${NEW_TAG}"

  keep_previous="$(cat .previous-tag 2> /dev/null || true)"
  # Newest first; keep current, previous and KEEP_IMAGES-2 older tags for manual rollback.
  docker image ls "$IMAGE" --format '{{.Tag}}' \
    | { grep -vxF -e "$NEW_TAG" -e "${keep_previous:-$NEW_TAG}" || true; } \
    | tail -n "+$((KEEP_IMAGES - 1))" \
    | while read -r old_tag; do docker image rm "${IMAGE}:${old_tag}" > /dev/null || true; done
  exit 0
fi

echo "Health check failed for ${IMAGE}:${NEW_TAG}" >&2
docker compose logs --tail=100 web >&2 || true

if [[ -n "$current_tag" && "$current_tag" != "$NEW_TAG" ]]; then
  echo "Rolling back to ${IMAGE}:${current_tag}" >&2
  write_env "$current_tag"
  docker compose up -d --remove-orphans
  if wait_healthy; then
    echo "Rollback to ${current_tag} is healthy." >&2
  else
    echo "Rollback to ${current_tag} is NOT healthy; manual intervention required." >&2
  fi
fi
exit 1
