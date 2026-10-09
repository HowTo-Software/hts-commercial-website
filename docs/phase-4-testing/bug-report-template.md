# Reporting an HTS frontend bug

> Status: Draft reporting guide. Owner: HTS team. Historical frontend baseline: 2026-10-02. Current edition: Unreleased.

Provide the following information in an issue or team report:

- **Title:** visible failure and affected flow, such as “Wizard review button clips at 150% zoom.”
- **Environment:** branch/commit, browser/version, operating system, viewport, zoom, theme and language.
- **Steps:** starting state, selected service, wizard step and exact interactions using synthetic values.
- **Expected/actual:** describe the reachable control, preserved answer or validation result expected, and what happened.
- **Evidence:** cropped screenshots or sanitized console/server output. Do not attach personal briefs, emails, phone numbers or tokens.
- **Impact:** blocking export, lost answers, inaccessible control or cosmetic issue; note affected languages and widths.
- **Retest:** identify the corrected build and repeat the same steps after a fix.

Treat lost data, unreachable final actions, misleading submission status and security defects as high priority. Minor visual spacing without interaction impact is lower priority. Report exploitable vulnerabilities privately under [SECURITY.md](../../SECURITY.md), not through a public bug reproduction containing sensitive data.

**PT-BR:** informe zoom, largura, tema, idioma e etapa; use dados fictícios e explique o comportamento esperado.
