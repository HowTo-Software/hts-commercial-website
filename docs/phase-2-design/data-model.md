# Data model and lifecycle

> Status: Draft, pending HTS review. Owner: HTS team. Historical frontend baseline: 2026-10-02. Current edition: Unreleased.

There is no relational/entity database model. `ProjectBrief` contains one `QuoteRequest`, scoped to the current Blazor circuit. This document describes application state rather than tables or migration scripts.

| Data group | Purpose | Handling |
| --- | --- | --- |
| Contact and business | Name, email, phone, business context | Personal/contact information; constrain lengths and format |
| Project context | Selected service, site type, new/existing mode and optional current URL | Use known choice values; validate provided URLs |
| Scope | Goals, audience, pages, features and description | Bounded user text and allowlisted choices |
| Visual direction | Style, colors, references, assets availability, languages | References are text; no remote fetch or upload |
| Planning | Integrations, domain/hosting, budget and timing | Indicative choices; no credentials or commercial commitment |
| UI state | Active step, validation/preparation flags | Component state, not part of a persisted record |

## Lifecycle

Service cards update the shared draft, and form edits modify that draft in memory. Navigation between wizard steps does not require database writes. Export creates a UTF-8 file in the visitor's browser. A new circuit, expired connection or process restart may discard the draft; the downloaded file is outside server retention control.

Theme preference is stored under `hts-theme`; culture preference uses the ASP.NET localization cookie. Neither is a customer-draft persistence mechanism. No migrations, indexes, seeded customer data or backup jobs exist for request answers. If durable storage is introduced, first define ownership, access control, consent/lawful basis, retention and deletion procedures.

**PT-BR:** os dados são campos do modelo em memória, não tabelas. O arquivo baixado passa a ficar sob responsabilidade do cliente.
