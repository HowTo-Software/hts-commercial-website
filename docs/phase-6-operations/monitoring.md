# Monitoring and observability

> Status: Proposed, not instrumented as a production monitoring system. Owner: HTS team. Historical frontend baseline: 2026-10-02. Current edition: Unreleased.

Current `appsettings` configures ASP.NET Core logging. No metrics exporter, external analytics, alerting, uptime dashboard or on-call integration is installed. Monitoring must be configured for the chosen hosting platform.

## What to observe

- HTTP availability and errors for the page and local assets.
- Blazor connections, disconnect/reconnect failures and unhandled circuit exceptions.
- Process memory, CPU and concurrent-circuit growth under realistic interactions.
- Deployment revision, restarts and certificate expiry.
- Browser smoke-test completion of wizard review and TXT download, which cannot be inferred from a successful `GET /`.

Use synthetic tests. Do not log contact fields, free-text answers, downloaded contents or request bodies. Restrict log access and define retention with HTS. Correlation IDs and technical error context are preferable to personal data.

## Alerts and objectives

Set alerts from measured staging behavior, capacity and the team's real response availability. Thresholds and uptime objectives are pending; this document does not invent an SLA. Each alert should name the affected flow, link to the [runbook](runbook.md) and have an assigned responder. If anonymous wizard usage metrics are later added, review consent/legal requirements and avoid combining event data with brief answers.

**PT-BR:** monitoramento está proposto, sem ferramenta ou SLA fictícios. Uma página 200 não prova que o assistente interativo funciona.
