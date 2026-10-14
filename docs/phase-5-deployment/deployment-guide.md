# Deployment guide

> Status: Docker deployment to `192.168.1.206`, published as `howto.software` through Caddy on `192.168.1.210`, is automated by the [CI/CD pipeline](ci-cd.md). Owner: HTS team. Historical frontend baseline: 2026-10-02. Current edition: Unreleased.

## Build artifact

Run from the project directory:

```powershell
dotnet restore
dotnet build -c Release --no-restore
dotnet publish HowToSoftware.Site.csproj -c Release --no-restore -o artifacts/publish
```

The framework-dependent artifact requires a compatible ASP.NET Core runtime on the selected host. `artifacts` is ignored by Git. Preserve the exact revision and artifact for rollback; do not publish developer browser profiles or local configuration files.

The production deployment uses the [`Dockerfile`](../../Dockerfile), which bundles the .NET 8 runtime. The [CI/CD pipeline](ci-cd.md) builds it on the self-hosted runner `192.168.1.210` and runs it with Docker Compose on `192.168.1.206` in `/opt/hts-commercial-website`. Images are tagged with the commit SHA and kept on the host for rollback.

## Hosting

Public traffic flows Cloudflare → Caddy on `192.168.1.210` (TLS, WebSockets) → container on `192.168.1.206:5180`. The app honors Caddy's forwarded headers (`ASPNETCORE_FORWARDEDHEADERS_ENABLED`), and `AllowedHosts` restricts the accepted hostnames. The container runs as the unprivileged `app` user. Confirm Blazor reconnect behavior and proxy/Cloudflare timeouts under real use. Keep operational secrets in the platform's secret store.

Use `Production` environment in production. Test the production exception path before launch: the baseline configures `/Error`, and a working safe response must be verified rather than assumed from Development-mode testing. No custom health endpoint or database migration exists in the baseline.

## Verify and roll back

Deploy first to staging. Check `/`, local assets, both themes, cultures, interactive wizard controls, complete review and TXT export. Confirm no request appears falsely delivered. Observe errors and reconnects while validating the production configuration.

On failure, restore the previous immutable artifact and matching settings. For the Docker host, `deploy/deploy.sh` does this automatically, and manual rollback is described in the [CI/CD guide](ci-cd.md#deploying-and-rolling-back). Restarts discard active circuit drafts; inform users about this limitation before planned work. Multiple instances need connection/session handling and measured capacity; a replica count alone is not a durability guarantee. The [runbook](../phase-6-operations/runbook.md) covers incidents.

**PT-BR:** o deploy Docker para `192.168.1.206` é automatizado pelo pipeline e publicado como `howto.software` pelo Caddy em `192.168.1.210`. Não há migração de banco neste projeto.
