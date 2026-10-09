# User stories and acceptance criteria

> Status: Draft, pending HTS review. Owner: HTS team. Historical project baseline: 2026-09-22. Current edition: Unreleased.

| Story | Customer need | Acceptance criteria |
| --- | --- | --- |
| US-01 | “I want a website and need guidance describing it.” | Five labeled steps ask contact/project, scope, style and planning; progress is visible |
| US-02 | “I already have a website and need maintenance.” | Selecting maintenance uses existing-project mode; optional URL accepts only HTTP/HTTPS |
| US-03 | “I am unsure which details are necessary.” | Questions include clear labels, optional answers and uncertainty options where suitable |
| US-04 | “I want to revise my answers.” | Back and review edit controls preserve answers in the same circuit |
| US-05 | “I need a complete file to discuss with HTS.” | Validated UTF-8 download includes each step and says no automatic delivery occurred |
| US-06 | “I use a phone, keyboard or another language.” | Mobile layout, focus, error messages and PT-BR/EN/ES work in light/black themes |
| US-07 | “I selected a service while browsing.” | Wizard keeps the service-prefill behavior from cards and project concepts |

## Primary use case

A visitor explores the landing page, chooses a service and starts the brief. The visitor fills each stage, resolves errors, reviews the answers, revisits a stage if necessary and downloads the file. The resulting file is on the visitor's device; HTS receives nothing automatically. A reload or expired circuit is an alternate flow that can discard the in-memory draft.

Trace story coverage in [test cases](../phase-4-testing/test-cases.md). Future “submit my request” stories must remain outside the acceptance criteria until an actual service and data-handling policy exist.

**PT-BR:** o fluxo termina no download local; qualquer envio real precisará de uma história e integração próprias.
