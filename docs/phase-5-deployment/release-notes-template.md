# Preparing release notes

> Status: Draft release-writing guide. Owner: HTS team. Historical frontend baseline: 2026-10-02. Current edition: Unreleased.

When HTS actually publishes a release, write notes using this sequence:

1. **Revision/date:** actual commit or tag and real publication date. Do not reuse a historical baseline date for new work.
2. **English explanation:** describe the customer problem and final behavior, for example the guided custom-site brief and review/download flow.
3. **PT-BR explanation:** provide equivalent meaning for the team and customers.
4. **Compatibility:** state changes to fields, localization, hosting or runtime; clarify whether existing circuits will be reset.
5. **Validation:** include actual build and browser evidence, with unresolved limitations.
6. **Deployment/rollback:** identify the artifact, staging check and recovery path.
7. **Data handling:** say explicitly whether submission/persistence exists; do not call a local download a delivered request.

For this iteration, the useful headline is “Guided custom website requests / Pedido guiado de site personalizado.” The work remains **Unreleased** until publication. There are no database changes or installed delivery service to announce. Link to [CHANGELOG.md](../../CHANGELOG.md), the reviewed revision and actual artifacts when they exist.

**PT-BR:** notas devem refletir o resultado final e evidências reais, sem inventar versão, aprovação ou data antiga.
