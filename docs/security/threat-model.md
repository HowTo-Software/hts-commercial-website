# Threat model

> Status: Draft assessment, not a security certification. Owner: HTS team. Historical frontend baseline: 2026-10-02. Current edition: Unreleased.

## Assets and boundaries

Protect service availability, contact/project answers, exported files, deployment credentials and the integrity of HTS content. Visitors are anonymous and their input is untrusted. The browser connects to server-side Blazor components; personal values cross that boundary even though there is no durable storage. The browser TXT file is a separate local copy.

| Threat | Current boundary/control | Residual work |
| --- | --- | --- |
| Input tampering | Model constraints and choice validation in server circuit | Revalidate complete draft before export; test invalid choice values |
| Script/content injection | Razor encodes displayed answers; export is plain text | Do not add trusted HTML or active links from arbitrary input |
| Unsafe URL processing | HTTP/HTTPS references, no userinfo, bounded reference list; no server fetch | Preserve this boundary if previews/imports are added |
| Data disclosure | Circuit state, no request-body logging or durable brief store by design | HTTPS, host logs, privacy notice and operational access must be reviewed |
| Resource exhaustion | Each connection consumes server resources | Measure realistic load; configure platform anti-abuse and connection limits |
| False delivery/confusion | UI and docs state download-only handling | Avoid “request sent” copy until durable submission exists |
| Operational compromise | No client-side deployment credentials required | Least-privilege hosting credentials, secret storage and release review |
| Draft loss | No recovery guarantee is presented | Validate reconnect/restart behavior; add durable recovery only with a separate design |

## Changes that require reassessment

A future request API, uploaded logo, persistent customer draft, email/CRM sender or remote reference preview introduces new data/access boundaries. Define authorization, abuse limits, retention and deletion before implementing them. Do not allow the site to fetch arbitrary URLs just to enrich a brief; that creates a server network access risk.

Razor/model controls are not proof of a production security audit. Confirm the actual deployed TLS/proxy configuration, error handling, runtime patches and circuit isolation before public launch. Vulnerability handling is described in [SECURITY.md](../../SECURITY.md).

**PT-BR:** o cliente não é confiável; validação ocorre no servidor. Upload, envio e persistência futuros precisam de outra revisão de ameaça.
