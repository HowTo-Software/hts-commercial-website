# Deployment guide

> Status: Proposed procedure; production infrastructure is not configured by this document. Owner: HTS team. Historical frontend baseline: 2026-10-02. Current edition: Unreleased.

## Build artifact

Run from the project directory:

```powershell
dotnet restore
dotnet build -c Release --no-restore
dotnet publish HowToSoftware.Site.csproj -c Release --no-restore -o artifacts/publish
```

The framework-dependent artifact requires a compatible ASP.NET Core runtime on the selected host. `artifacts` is ignored by Git. Preserve the exact revision and artifact for rollback; do not publish developer browser profiles or local configuration files.

## Hosting decisions before deployment

HTS must select hosting, staging, domain, certificate ownership and release access. Confirm long-lived Blazor connections/WebSockets, reconnect behavior and proxy timeouts. Terminated TLS and forwarded headers must be configured together for the actual proxy; do not assume local HTTP settings are production-ready. Restrict allowed hostnames, use an unprivileged runtime identity and keep operational secrets in the platform's secret store.

Use `Production` environment in production. Test the production exception path before launch: the baseline configures `/Error`, and a working safe response must be verified rather than assumed from Development-mode testing. No custom health endpoint or database migration exists in the baseline.

## Verify and roll back

Deploy first to staging. Check `/`, local assets, both themes, cultures, interactive wizard controls, complete review and TXT export. Confirm no request appears falsely delivered. Observe errors and reconnects while validating the production configuration.

On failure, restore the previous immutable artifact and matching settings. Restarts discard active circuit drafts; inform users about this limitation before planned work. Multiple instances need connection/session handling and measured capacity; a replica count alone is not a durability guarantee. The [runbook](../phase-6-operations/runbook.md) covers incidents.

**PT-BR:** publicação exige decisões de infraestrutura e smoke test em staging; não há migração de banco neste projeto.
