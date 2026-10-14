# Test plan

> Status: Draft verification plan, not a passed-test report. Owner: HTS team. Historical frontend baseline: 2026-10-02. Current edition: Unreleased.

## Scope and environments

Executed local wizard checks are recorded in [verification.md](verification.md). They cover the implemented flow; remaining release checks below still require execution.

Test wizard navigation, per-step validation, complete review/export, service-prefill regressions and truthful download-only copy. Include the existing hero, filters, dialogs, menu, culture/theme persistence and motion behavior. No delivery API or payment integration should be tested as if implemented.

Use synthetic names, emails and project details. Test PT-BR, EN and ES in light and black themes at 320, 375, 768, 1280 and 1440 px; add browser zoom at 120%, 150% and 200%. Record browser version, viewport, theme, culture, commit/worktree state and actual execution result.

## Levels and exit criteria

The Release build checks Razor/C# compilation. Browser tests check actual interactions, layout and downloaded contents. Model tests are useful for field lengths, allowed values and cross-step validation; add focused tests when the behavior requires them, rather than describing a nonexistent suite.

Exit requires a passing build and all high-priority [cases](test-cases.md), with no missing wizard answers, false submission message or unreachable controls. Failed cases need reproduction steps and a linked fix/retest record. Screenshots alone cannot verify export contents or data validation.

Cloud readiness additionally requires staging smoke tests, disconnect/reconnect checks and measured load against the chosen deployment. No capacity, concurrency target or production uptime is asserted by this plan. The [CI/CD guide](../phase-5-deployment/ci-cd.md) describes the installed pipeline and its container smoke test.

**PT-BR:** este é o plano de testes, não resultados inventados. Registre build, navegação real e conteúdo do TXT.
