# Wizard test cases and regressions

> Status: Draft cases; execution evidence must be recorded separately. Owner: HTS team. Historical frontend baseline: 2026-10-02. Current edition: Unreleased.

| ID / priority | Exercise | Expected result | Traces |
| --- | --- | --- | --- |
| TC-01 / High | Start with empty step 1; choose Next | Relevant localized errors; no advancement | FR-03/04, US-01 |
| TC-02 / High | Supply valid contact/project and complete all steps | Progress reaches review; entered values remain intact | FR-03/04, US-04 |
| TC-03 / High | Choose maintenance in the service section | Wizard keeps that service and existing-project mode | FR-01/02, US-02/07 |
| TC-04 / High | Enter `https://example.com`, blank optional URL and unsafe/invalid URL text separately | HTTP/HTTPS and blank optional URL accepted; invalid supplied URL rejected | FR-05, US-02 |
| TC-05 / High | Fill scope, style, languages and planning; review; edit each stage | Summary updates; all fields survive Back/edit flow | FR-06, US-04 |
| TC-06 / High | Download a valid brief in each language | UTF-8 readable text, localized labels, all relevant answers and local-only notice | FR-07/08/09, US-05 |
| TC-07 / High | Enter text beyond constraints or modify choices outside allowed values in model-focused checks | Validation fails; export cannot bypass full-model validation | FR-04/07 |
| TC-08 / High | Navigate keyboard-only; trigger errors and use review edit | Focus is visible; labels/errors associated; controls reachable | PR-07, US-06 |
| TC-09 / High | Test narrow widths and 120–200% browser zoom in both themes | No horizontal overflow, clipped actions or illegible error text | PR-07, US-06 |
| TC-10 / Medium | Select service after partially filling the wizard | Contact/scope retained; service changes reflected consistently | FR-01, US-07 |
| TC-11 / Medium | Change language/theme and reload | Preferences work; copy explains circuit draft limitations | FR-08, US-06 |
| TC-12 / Medium | Enable reduced motion; exercise hero, dialogs, filters and mobile menu | Content visible, expected keyboard/motion behavior retained | PR-07 |
| TC-13 / High | Review/download text containing `<script>` or markup-like characters | Text displayed/exported as text; no execution | Security model |
| TC-14 / Medium | Reload, interrupt connection or restart local app while drafting | No false saved/submitted status; circuit limitation is understandable | Data lifecycle |

Record each result as **pass**, **fail** or **not executed**, alongside the actual environment and evidence. The historical preview images are not a pass result for these cases. Use only synthetic customer information. A budget choice is an indicative answer; it must not create a calculated quote or payment.

**PT-BR:** priorize validação, preservar respostas, revisão/TXT completo e layout em zoom. Ausência de execução não pode ser marcada como aprovado.
