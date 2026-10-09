# Writing the next HTS architecture decision

> Status: Draft writing guide, pending HTS review. Owner: HTS team. Historical frontend baseline: 2026-10-02. Current edition: Unreleased.

Create a numbered record only when there is a concrete decision to discuss. Use these headings without leaving blank project placeholders:

1. **Title and status:** state the decision; mark proposed or accepted according to an actual review.
2. **Context:** describe the problem, affected customer flow and existing code boundary.
3. **Approach:** name the components/interfaces changed and what the customer will experience.
4. **Alternatives:** include relevant options and why the selected approach fits.
5. **Consequences:** explain state lifetime, security/privacy, operations and migration effects.
6. **Evidence:** link to actual code, tests and review; use real dates for those events.

For a future brief-delivery API, explicitly answer who receives the request, where it is stored, who may read/delete it, how retries work and how the UI distinguishes accepted, pending and failed delivery. Add the record to the [index](README.md).

**PT-BR:** preencha o registro com decisões e evidências reais; não transforme a data histórica do projeto em data fictícia de aprovação.
