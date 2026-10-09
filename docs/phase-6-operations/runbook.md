# Operations runbook

> Status: Proposed operations guidance; production host/on-call pending HTS decision. Owner: HTS team. Historical frontend baseline: 2026-10-02. Current edition: Unreleased.

The service is one ASP.NET Core/Blazor application serving local assets and maintaining interactive circuits. No database, queue, email provider or submission worker needs routine operation in this iteration.

## Incident playbooks

| Symptom | Check | Response |
| --- | --- | --- |
| Page unavailable | Runtime process, reverse proxy, TLS, artifact and platform logs | Restore a known-good revision/configuration; verify `/` and assets |
| Page loads but wizard is inert | Blazor connection, browser console, WebSocket/proxy support and server exceptions | Correct connection routing or revert failed deployment; check an actual Next/Back action |
| Draft disappears | Reload, restart, circuit expiration or instance routing | Explain circuit lifetime; do not claim a nonexistent database can recover it |
| TXT missing fields/wrong language | Wizard revision, resources and download helper | Reproduce with synthetic data, block/revert the affected release and retest export |
| Layout clips at zoom | Browser/viewport/theme/culture and current CSS | Reproduce exact dimensions; follow the frontend bug report guide |

## Routine work

Before restarting or deploying, remember active drafts may be lost. Retain the previous artifact, observe errors and run the staging/production smoke tests. Define platform-specific start/stop procedures after hosting is selected; there is no installed service manager or operational dashboard to reference yet.

Review dependencies, TLS renewal, attribution, logs and privacy copy with the HTS team. Record incident start/end, impacted flows, actual cause, actions and follow-up without copying customer answers. Assign a real escalation channel and operational owner before public launch.

**PT-BR:** reiniciar pode apagar rascunhos em memória. Verifique conexão Blazor, não apenas o carregamento da página.
