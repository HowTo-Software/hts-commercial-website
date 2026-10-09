# Coding standards

> Status: Draft, pending HTS review. Owner: HTS team. Historical frontend baseline: 2026-10-02. Current edition: Unreleased.

- Keep Razor components focused on display/event orchestration and model constraints in C#. Use the existing `HowToSoftware.Site` namespaces and PascalCase for public C# members.
- Use semantic labels, buttons, fieldsets and validation associations. Preserve keyboard access, visible focus and reduced-motion behavior. Avoid clickable containers substituting for buttons.
- Keep customer-visible text in resources, with equivalent PT-BR, EN and ES keys. Export option labels in the selected language; do not leak localization keys or internal enum codes into customer documents.
- Retain the existing CSS variables, theme selectors and component class naming. Test long translated labels and narrow widths before adding fixed dimensions.
- Use one scoped `ProjectBrief`; avoid static customer state or shared singleton request data. Unsubscribe component event handlers on disposal.
- Bound free text and validate allowed choices server-side. Render plain encoded text. Do not use `MarkupString`, `innerHTML` or executable URL construction for customer answers.
- Keep JavaScript for existing browser tasks such as download, dialogs and preferences. Release object URLs when finished; keep interop arguments as data.
- No passwords, API tokens or secrets belong in the wizard, export, screenshots or logs. Do not fetch customer reference URLs on the server.
- Avoid new frameworks/dependencies for behavior already supported by the current runtime. Preserve third-party attribution and local asset licenses.

Before review, run the Release build and relevant test cases; update the changelog and design docs for actual behavior changes. English is the principal change description; include PT-BR for the team.

**PT-BR:** mantenha o modelo compartilhado, traduções equivalentes, validação no servidor e conteúdo do cliente tratado como texto.
