# Security Policy

> Status: Draft; security contact and production support policy pending HTS review. Owner: HTS team. Historical frontend baseline: 2026-10-02. Current edition: Unreleased.

## Scope

The repository contains a public commercial Blazor site and an in-memory request wizard. There is no customer account system, persistence database, payment API or request-delivery endpoint. The absence of durable storage does not mean information stays only in the browser: interactive server components process entered values in the server's Blazor circuit.

## Reporting

Report vulnerabilities privately to a known HTS maintainer through the team's existing private channel. If the repository host offers private vulnerability reporting, use it only when enabled. Do not post exploitable details, customer data or credentials in public issues. No dedicated security mailbox, response SLA or supported version matrix has been approved; HTS must establish them before production operation.

Include the affected commit, hosting configuration, reproduction steps, expected impact and suggested mitigation. Use synthetic contact data in examples. Request coordination before public disclosure.

## Implementation and deployment responsibilities

- Keep Razor output encoded; never render customer input as trusted HTML.
- Validate request values on the server; preserve length limits and allowed choices. Existing/reference URLs are textual references and must not trigger untrusted server fetches.
- Keep credentials out of resources, app settings, exports and repository history.
- Use HTTPS, correctly configured reverse-proxy handling and an unprivileged process identity in the selected hosting environment.
- Treat exported briefs as personal information and avoid request-body logging.
- Review the [threat model](docs/security/threat-model.md) before adding submission, storage or uploads.

**PT-BR:** o briefing atual não é enviado nem persistido, mas os valores são processados no circuito Blazor do servidor. Não inclua senhas ou dados sensíveis. Vulnerabilidades devem ser relatadas em particular à equipe HTS; contato e política operacional precisam ser definidos antes da produção.
