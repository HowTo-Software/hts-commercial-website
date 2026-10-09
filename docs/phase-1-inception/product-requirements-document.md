# Product requirements — custom website brief

> Status: Draft, pending HTS review. Owner: HTS team. Historical frontend baseline: 2026-10-02. Current edition: Unreleased.

The brief turns a generic contact form into a guided conversation about a custom site. It retains the site's editorial composition rather than introducing a separate application or a new design system.

| ID | Requirement | Priority |
| --- | --- | --- |
| PR-01 | Capture contact, business, service and new/existing project context | Must |
| PR-02 | Ask goals, audience, pages and necessary features | Must |
| PR-03 | Ask style, colors, references, available materials and languages | Must |
| PR-04 | Ask integrations, domain/hosting status, budget and timeline | Must |
| PR-05 | Provide progress, Back/Next and editable review before download | Must |
| PR-06 | Preserve selected services and maintenance prefill | Must |
| PR-07 | Preserve accessible labels, focus, localized errors and responsive themes | Must |
| PR-08 | Deliver the request to a backend or CRM | Future, separate scope |

Optional answers should accept uncertainty: prospects may not know hosting, budget or design terminology. Required questions must have localized errors and server-side constraints. Input must never be framed as an approved commercial contract.

## Release criteria and open decisions

Required criteria: Release build passes, critical [cases](../phase-4-testing/test-cases.md) pass, all answers appear in review/export and copy distinguishes preparation from submission. Hosting, real request delivery, privacy publication, supported operating model and project license remain decisions for HTS.

No analytics provider is installed. Any future wizard-completion metric should record an event count without logging contact details or free-text answers. Targets require an observed baseline rather than invented conversion figures.

**PT-BR:** critérios principais: perguntas claras, revisão editável, todas as respostas no TXT e nenhuma promessa de envio inexistente.
