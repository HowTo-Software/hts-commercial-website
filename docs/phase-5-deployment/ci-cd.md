# Proposed CI/CD pipeline

> Status: Proposed, not installed. Owner: HTS team. Historical frontend baseline: 2026-10-02. Current edition: Unreleased.

The baseline has no checked-in CI/CD workflow. This document defines the pipeline HTS can implement after choosing the repository host and deployment platform; it does not enable deployment or declare passing checks.

| Stage | Required evidence |
| --- | --- |
| Checkout and toolchain | Exact commit and .NET 8 SDK |
| Restore | `dotnet restore` completes |
| Build | Release build succeeds; review warnings |
| Checks | Relevant model/browser checks and localization consistency; do not treat zero discovered tests as coverage |
| Publish | `dotnet publish` creates an artifact tied to the commit |
| Staging | Deployment under least-privilege credentials and smoke tests |
| Production | HTS-reviewed artifact promotion and rollback path |

Browser automation, test projects and deployment jobs need actual implementation. Include a realistic connection/reconnect check for interactive Blazor. Keep customer answers out of test output; use synthetic briefs and temporary artifact storage.

Deployment credentials belong to the host's secret store, scoped to the required environment. PR checks should not require production credentials. Prefer a platform-supported short-lived identity if available; avoid printing credentials in logs. Cache dependency/tool data, not customer state or secrets.

Document English and PT-BR release explanations. Configure branch/environment protection and reviewer requirements in the actual host before describing them as enforced. A failed staging smoke test blocks promotion; retaining the previous artifact supports rollback.

**PT-BR:** este pipeline é uma proposta documentada, ainda sem workflow instalado ou deploy habilitado.
