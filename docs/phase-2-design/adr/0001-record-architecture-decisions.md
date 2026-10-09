# ADR-0001: Keep the request wizard in the existing Blazor site

> Status: Observed baseline and Unreleased implementation direction; HTS review pending. Owner: HTS team. Historical runtime baseline: 2026-09-22; frontend baseline: 2026-10-02.

## Context

The commercial site already uses .NET 8, localized Razor sections and a scoped draft shared by service cards and the quote form. Hank requested a wizard or question list for custom websites, and the user requested preserving the current frontend.

## Approach

Extend `QuoteForm`, `QuoteRequest`, localization resources and existing CSS to support five stages. Keep `ProjectBrief` as the scoped source of truth. Continue exporting the validated brief through the existing browser TXT helper. Record architectural decisions alongside the code; this record does not establish a historic approval date.

## Consequences

The UI keeps its existing theme, localization and service-prefill behavior, without an additional frontend framework. State remains tied to the server circuit; reload/restart recovery and real submission are unavailable. The application must continue to communicate those limitations honestly.

A separate React application would duplicate the current runtime and design. An immediate CRM/database submission would add a new privacy and security boundary beyond the requested wizard. Both remain alternatives requiring separately approved scope, rather than part of this change.

**PT-BR:** manter Blazor, o modelo compartilhado e o TXT reduz duplicação; envio e persistência exigem outra decisão.
