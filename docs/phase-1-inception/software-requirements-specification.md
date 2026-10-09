# Software requirements specification

> Status: Draft, pending HTS review. Owner: HTS team. Historical frontend baseline: 2026-10-02. Current edition: Unreleased.

## Functional requirements

| ID | Verifiable behavior |
| --- | --- |
| FR-01 | Selecting a service populates the same `ProjectBrief` used by the wizard |
| FR-02 | Maintenance selects an existing project; other project choices remain available |
| FR-03 | Five steps separate project/contact, scope, style, planning and review |
| FR-04 | Next validates the relevant answers; Back and editing preserve draft state in the circuit |
| FR-05 | Provided existing/reference website addresses are validated as HTTP/HTTPS references |
| FR-06 | Review lists all relevant answers and provides an edit route |
| FR-07 | Download revalidates the complete model and exports UTF-8 text |
| FR-08 | UI and validation work in PT-BR, EN and ES |
| FR-09 | Preparing/downloading a brief does not show a false “sent to HTS” state |

## Constraints and quality requirements

Use .NET 8 and interactive server-rendered Razor components. Draft lifetime is scoped to the Blazor circuit. There is no persistence migration or public quote API in this iteration. Keep all user-visible resources localizable, limit text lengths, allowlist choice values and use default Razor encoding.

At 320 px, desktop widths and high browser zoom, users must reach every input and navigation button without horizontal overflow. Both themes must preserve readable text and visible focus. Reduced motion must not conceal content. Do not insert customer answers into HTML, URLs, JavaScript or logs as trusted text.

Verify requirements with [test cases](../phase-4-testing/test-cases.md); performance/load acceptance remains dependent on a selected deployment and measured traffic. Browser storage contains theme preference, not a persisted customer draft.

**PT-BR:** o rascunho existe no circuito do servidor e não em banco. Voltar preserva o estado atual; recarregar ou perder a sessão pode descartá-lo.
