# Technical design — website-request wizard

> Status: Draft, implementation/review in Unreleased. Owner: HTS team. Historical frontend baseline: 2026-10-02.

## Flow and component responsibilities

`QuoteForm.razor` remains inside the existing `#quote` section and uses the current cards, typography, labels and button styles. It owns the step index, edit context, validation feedback, review rendering and download action. `QuoteRequest` owns the answers; `BriefOptions` defines allowed choices, `BriefValidation` validates stages/the complete model and `BriefFormatter` prepares localized export text. `ProjectBrief` remains the shared scoped service for external service selections.

| Step | Questions | Navigation expectation |
| --- | --- | --- |
| 1 | Contact, business, selected service, site type, new/existing context and optional URL | Validate contact and project context |
| 2 | Goals, audience, pages and features | Preserve choices and free-text details |
| 3 | Style, colors, references, available content/brand assets and languages | Ask about availability; do not upload assets |
| 4 | Integrations, domain/hosting, budget and timeline | Allow uncertainty; do not promise hosting provisioning |
| 5 | Full answer summary | Edit a previous step and revalidate before export |

Next validates only the active stage. Back preserves answers. Final download validates the entire model, and an earlier failure must provide a way to revisit its stage. Changing an answer after preparation invalidates the prepared state. Maintenance/service-card changes must retain the current prefill event flow.

## State and output

Use a single draft model rather than separate, diverging models per stage. Allowlist select/multiple-choice values and apply text limits server-side. Reference URLs are limited to five non-empty lines, require HTTP/HTTPS and reject embedded user information. Avoid network calls for these URLs; they are descriptive text. Review must use encoded Razor output. Export must include the same relevant answers and localized option labels, with an explicit local-download notice.

Active input parsing errors also block advancement, even if the model retains a previous valid value. `BriefDateInput` clears its owned parsing messages when the conditional date input is disposed, preserving the last valid date and preventing obsolete errors after hiding or revisiting a stage.

No endpoint or database migration is needed. No user-entered filename is used; JavaScript creates the existing local UTF-8 TXT download. See [test cases](../phase-4-testing/test-cases.md) for required regressions. Durable recovery, submission, file handling and payment remain separate future designs.

**PT-BR:** um modelo compartilhado alimenta as cinco etapas e o TXT. Perguntar sobre materiais não significa enviar arquivos; orçamento e prazo são preferências do cliente.

## Motion refinement — Unreleased

Status: implemented in Unreleased; HTS review pending. Fresh local motion checks are recorded in [verification](../phase-4-testing/verification.md), including remaining limits. Retain the current typography, colors, layout and existing Skiper/OriginKit attribution. Additional motion is HTS-owned CSS/JavaScript behavior, without another animation framework.

| Scene | Intended motion | Boundary |
| --- | --- | --- |
| Hero preview | Gentle float with a restrained decorative highlight/star | Separate float, reveal and tilt wrappers so transforms do not override each other |
| Capabilities band | Slow continuous horizontal track | One semantic copy, decorative repetition, readable static fallback and an independent persistent pause |
| Service art | Slow orbit/rotation of decorative shapes | No moving service labels or form controls |
| Sections/cards/workflow | Short staged entrance and hover/focus response | Content remains present; no delayed interaction or repeated entrance on normal form edits |
| Wizard | Short step transition | Focus/validation updates stay immediate; answers and navigation are unchanged |

### Motion control and accessibility

`MotionToggle.razor` provides reusable controls at the hero and capabilities band; both change one global preference rather than requiring the user to remain hovering or focused. Store `full` or `paused` under `hts-motion` when browser storage permits; storage failure must not break the control. `prefers-reduced-motion: reduce` takes priority, including runtime OS changes. Paused/reduced mode removes non-essential movement, leaves all answers/actions visible and renders the complete original capability group in a wrapping static layout, hiding the duplicate.

Hover and focus pause the band temporarily as an additional convenience. They cannot be the only stop mechanism for automatically moving informational content: a user must be able to continue navigating after stopping it. This follows the [W3C explanation of Pause, Stop, Hide](https://www.w3.org/WAI/WCAG22/Understanding/pause-stop-hide.html). The global control also suppresses non-essential interaction motion, consistent with [Animation from Interactions guidance](https://www.w3.org/WAI/WCAG22/Understanding/animation-from-interactions.html). These safeguards are a design contract, not a claim of WCAG certification.

Decorative duplicates use `aria-hidden` and contain no focusable descendants. Do not add a live region for the marquee. Keep the semantic capabilities copy available once, independent of animation. Controls require translated accessible labels, visible focus and a state reflecting the effective motion preference.

### Runtime and performance

Activate looping effects only after the motion controller initializes; JavaScript failure leaves static content and the original capability labels visible. This fallback does not implement a non-interactive replacement for the wizard: step navigation, validation feedback and download require the Blazor JavaScript runtime and a live server connection. Observe scene visibility and pause loops outside the viewport. Hidden tabs pause decorative scenes according to `document.hidden`; resume only if the user's preference and OS permit it. Blazor-rendered nodes need registration/removal cleanup without duplicate handlers or unbounded observers.

The runtime contract uses `html[data-motion="full"]` or `html[data-motion="paused"]` for effective motion, and `data-page-hidden` set to `true` or `false` for document-visibility suspension. Each `[data-motion-scene]` receives `.is-motion-visible` from viewport observation. CSS combines those states to run or pause continuous scenes. The stable `.hero-showcase` parent is the hero's observed scene; its class state must survive Blazor's desktop/mobile preview rerender. The canvas owns pointer tilt, and `preview-float` owns the independent continuous transform. Focused reveal content must become visible immediately rather than waiting for a stagger, including when focus arrives after the entrance delay has already started.

Preview-mode and project-filter controls explicitly render `aria-pressed` as the strings `true` or `false`; treating it as an HTML boolean attribute would incorrectly remove the false state. This interaction state is distinct from the motion controller's effective pause state.

Use small transforms and opacity changes; avoid layout-size animations, large continuous blur/filter repaints and permanent compositor promotion of the whole page. Do not introduce scroll-position rewrites or hijacked navigation. With JavaScript and the circuit active, focus targets, validation feedback and downloads must work regardless of motion state. Fresh browser checks must cover pause persistence after focus moves, OS/storage changes, scene lifecycle and the existing wizard regression flow. No-script checks cover visible static content; they do not certify wizard functionality.

**PT-BR:** movimento fica na decoração e em transições curtas. A pausa explícita continua valendo após navegar; redução de movimento, conteúdo estático completo e foco funcional são requisitos, ainda sujeitos a testes desta edição.
