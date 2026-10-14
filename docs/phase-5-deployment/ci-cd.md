# CI/CD pipeline

> Status: Installed in `.github/workflows`; runner, SSH alias and GitHub environment configured on 2026-10-14. Owner: HTS team. Historical frontend baseline: 2026-10-02. Current edition: Unreleased.

## Overview

```text
visitor ─► Cloudflare ─► Caddy on 192.168.1.210 (TLS, howto.software) ─► container on 192.168.1.206:5180
```

| Host | Role |
| --- | --- |
| GitHub-hosted `ubuntu-latest` | CI for every pull request and branch push. Untrusted code never runs on the home network. |
| `192.168.1.210` (`2htswebserver`) | Self-hosted runner `2htswebserver-hts-site` (label `hts-deploy`, folder `~htsadmin/actions-runner-hts-site`). It builds the Docker image and deploys over the SSH alias `hts-site-deploy-206`. It also runs the edge **Caddy** proxy that serves `howto.software`. |
| `192.168.1.206` (`website-applications`) | Web host. Runs the site with Docker Compose from `/opt/hts-commercial-website` on port `5180` (container port 8080). |

```text
PR / branch push ──► CI (GitHub-hosted): restore → build -warnaserror → localization keys → publish artifact
                                          └► docker build → hardened container → smoke test

push to main ─────► CD: CI (same as above) ──► deploy job on the .210 runner (environment: production)
                         docker build → docker save | ssh hts-site-deploy-206 docker load → deploy.sh → smoke test
```

| File | Purpose |
| --- | --- |
| [`.github/workflows/ci.yml`](../../.github/workflows/ci.yml) | CI checks; also called by CD (`workflow_call`) |
| [`.github/workflows/cd.yml`](../../.github/workflows/cd.yml) | Runs CI, then deploys `main` through the `production` environment |
| [`Dockerfile`](../../Dockerfile) | Multi-stage .NET 8 build; non-root `app` user, port 8080 |
| [`deploy/docker-compose.yml`](../../deploy/docker-compose.yml) | Production stack: read-only filesystem, dropped capabilities, log rotation, persistent Data Protection keys, forwarded headers and `AllowedHosts` |
| [`deploy/deploy.sh`](../../deploy/deploy.sh) | Runs on `.206`. Switches the image tag, health-checks `http://127.0.0.1:5180/`, rolls back automatically and keeps a few older images |
| [`deploy/Caddyfile.howto.software`](../../deploy/Caddyfile.howto.software) | Versioned copy of the site block in the edge Caddyfile on `.210` |
| [`.github/scripts/smoke-test.sh`](../../.github/scripts/smoke-test.sh) | Checks `/`, the CSS, `blazor.web.js` and the `/_blazor/negotiate` circuit endpoint |
| [`.github/scripts/check-localization.py`](../../.github/scripts/check-localization.py) | Fails when `AppStrings.*.resx` keys drift from the neutral resource |

Images are tagged with the commit SHA. No registry is needed, because the image is streamed from `.210` to `.206` over SSH. The deploy key stays on `.210` and is not stored in GitHub.

## Configuration in place

This follows the same pattern as the Marks-BaseBallCards deployment.

- **.210 (`htsadmin`):**
  - The key `~/.ssh/id_ed25519_hts_site_deploy` is used by the `~/.ssh/config` alias `hts-site-deploy-206`, which connects as `htsadmin@192.168.1.206` with `BatchMode yes`.
  - The runner is in `~/actions-runner-hts-site`, separate from the existing panel and Marks runners.
- **.206 (`htsadmin`):** the deploy key is in `~/.ssh/authorized_keys`, restricted with `from="192.168.1.210"`. `htsadmin` is in the `docker` group.
- **GitHub:**
  - The `production` environment may only deploy from `main`.
  - Fork pull requests from all external contributors need approval before workflows run.
  - The runner is registered at repository level.

Two steps need `sudo`. Run them once:

```bash
# on 192.168.1.206
sudo install -d -o htsadmin -g htsadmin -m 750 /opt/hts-commercial-website

# on 192.168.1.210
cd ~/actions-runner-hts-site && sudo ./svc.sh install htsadmin && sudo ./svc.sh start
```

The runner then appears as **Idle** under Settings → Actions → Runners.

Optional repository or environment variables override the defaults: `DEPLOY_SSH` [`hts-site-deploy-206`], `DEPLOY_HOST` [`192.168.1.206`], `DEPLOY_PATH` [`/opt/hts-commercial-website`] and `HOST_PORT` [`5180`]. Optionally add required reviewers to the `production` environment. You can also require the `Build and check` and `Container build and smoke test` checks on `main`. Never change `ci.yml` to use the self-hosted runner, because this repository is public.

## Caddy on 192.168.1.210

The block in [`deploy/Caddyfile.howto.software`](../../deploy/Caddyfile.howto.software) lives in `/home/htsadmin/caddy/Caddyfile`, which is bind-mounted into the `caddy-proxy` container.

- `www.howto.software` redirects to the apex.
- `howto.software` proxies to `192.168.1.206:5180`. WebSockets for the Blazor circuit are proxied automatically.
- HSTS omits `includeSubDomains`, so `auth.`, `panel.` and `host.howto.software` are unaffected.

To change the block, edit the file in place (the bind mount is a single file, so do not replace it with `sed -i`). Then run:

```bash
docker exec caddy-proxy caddy validate --config /etc/caddy/Caddyfile --adapter caddyfile
docker exec caddy-proxy caddy reload --config /etc/caddy/Caddyfile --adapter caddyfile
```

`howto.software` is proxied by Cloudflare, so visitors see Cloudflare's certificate in front of Caddy's. The app trusts Caddy's `X-Forwarded-Proto`/`X-Forwarded-For` through `ASPNETCORE_FORWARDEDHEADERS_ENABLED`, which gives secure culture cookies and HSTS. `AllowedHosts` is limited to `howto.software`, `www.howto.software`, `192.168.1.206`, `127.0.0.1` and `localhost`.

## Deploying and rolling back

- **Normal release:** merge to `main`. CD runs CI, builds and deploys the image, then smoke-tests `http://192.168.1.206:5180` from `.210`.
- **Automatic rollback:** if `deploy.sh` cannot reach the new container within about 60 seconds, it restores the previous tag. If the later smoke test from `.210` fails, the workflow redeploys the tag in `.previous-tag`.
- **Manual rollback:** go to Actions → CD → Run workflow and enter an older commit SHA as `image_tag`. That image must still be on `.206` (`docker image ls hts-commercial-website`; the current, previous and three older tags are kept). Alternatively, on `.206`, run `cd /opt/hts-commercial-website && ./deploy.sh <sha>`.
- **Restart behavior:** a restart discards active Blazor circuits and in-progress briefs. Data Protection keys persist in the `hts-commercial-website_dataprotection-keys` volume, so antiforgery tokens survive restarts.

## Troubleshooting

| Symptom | Check |
| --- | --- |
| Deploy job stays queued | The `actions.runner.HowTo-Software-hts-commercial-website.*` service on `.210` is stopped (`systemctl status`) |
| `must exist on the web host and be writable` | Run the `sudo install -d …` command above on `.206` |
| `Permission denied (publickey)` | `ssh hts-site-deploy-206 true` as `htsadmin` on `.210`; check the `from=` restriction in `.206`'s `authorized_keys` |
| Health check failed | `docker compose -f /opt/hts-commercial-website/docker-compose.yml logs web` on `.206` |
| 400 "Bad Request - Invalid Hostname" | The request host is missing from `AllowedHosts` in `deploy/docker-compose.yml` |
| 502 at `howto.software` | The container is down on `.206:5180`, or Caddy cannot reach it (`docker logs caddy-proxy`) |

**PT-BR:** o CI roda em runners do GitHub. O CD roda no runner `2htswebserver-hts-site` (`192.168.1.210`, rótulo `hts-deploy`), envia a imagem Docker via SSH (`hts-site-deploy-206`) e publica com Docker Compose em `192.168.1.206:/opt/hts-commercial-website`, porta 5180, com smoke test e rollback automático. O Caddy em `192.168.1.210` publica `howto.software` para esse container.
