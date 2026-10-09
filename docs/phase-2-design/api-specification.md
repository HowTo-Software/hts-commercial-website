# Implemented interfaces and future API boundary

> Status: Draft reference; no request API implemented. Owner: HTS team. Historical frontend baseline: 2026-10-02. Current edition: Unreleased.

## Current HTTP surface

| Interface | Behavior |
| --- | --- |
| `GET /` | Commercial page and interactive Blazor components |
| `GET /culture?culture=en&returnUrl=%2F` | Sets culture preference and redirects locally |
| Static paths under `wwwroot` | CSS, JavaScript, images and fonts |
| Blazor framework connection | Framework-managed component interaction; not a public quote API |

Culture selection normalizes to PT-BR, EN or ES. The return target is limited to a local path; protocol-relative, backslash-prefixed or control-character targets must not become external redirects. The culture cookie uses a one-year expiration, `SameSite=Lax` and `Secure` when the request is HTTPS.

Wizard reference addresses allow up to five non-empty lines, use HTTP/HTTPS and reject embedded user information. They are validated descriptive inputs, not API targets or URLs fetched by the server.

There is **no** `POST /quotes`, request ID, delivery status, webhook, database API, auth token or email response. JavaScript's `hts.downloadBrief` produces a local file and is not an HTTP delivery contract.

## Future submission design — not installed

Before implementing delivery, HTS must approve the channel and define a server-validated schema, payload limits, anti-abuse controls, privacy/retention policy and a clear response model. A delivery confirmation must correspond to an accepted durable request, with retry/idempotency rules if external email or CRM delivery is involved. Public visitors must not receive operational credentials. Logging must exclude free-text and contact values by default.

Do not make frontend calls to proposed endpoints until they are implemented and tested. Update this document with actual paths, error schemas and limits at that time.

**PT-BR:** hoje existe apenas a página, preferência de idioma e conexão Blazor. O TXT não chama uma API de orçamento.
