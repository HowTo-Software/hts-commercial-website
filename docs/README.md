# HTS documentation

> Status: Draft, pending HTS review. Owner: HTS team. Historical project baseline: 2026-09-22; frontend baseline: 2026-10-02. Current edition: Unreleased.

The local `best-document` template supplies the lifecycle organization. Every document below is adapted to this Blazor commercial site; example accounts, databases, release badges and placeholder approvals were not copied.

## Lifecycle map

| Phase | Documents | Purpose |
| --- | --- | --- |
| 1 — Requirements | [Vision](phase-1-inception/vision-and-scope.md), [PRD](phase-1-inception/product-requirements-document.md), [SRS](phase-1-inception/software-requirements-specification.md), [stories](phase-1-inception/user-stories.md), [roadmap](phase-1-inception/product-roadmap.md) | Explain the custom-website brief and its boundaries |
| 2 — Design | [Architecture](phase-2-design/architecture.md), [wizard design](phase-2-design/technical-design.md), [data](phase-2-design/data-model.md), [interfaces](phase-2-design/api-specification.md), [diagrams](phase-2-design/diagrams.md), [ADRs](phase-2-design/adr/README.md) | Map the actual Blazor application and draft lifecycle |
| 3 — Development | [Onboarding](phase-3-development/onboarding.md), [standards](phase-3-development/coding-standards.md), [Git](phase-3-development/git-workflow.md) | Build and change the project consistently |
| 4 — Testing | [Plan](phase-4-testing/test-plan.md), [cases](phase-4-testing/test-cases.md), [executed verification](phase-4-testing/verification.md), [bug report](phase-4-testing/bug-report-template.md) | Verify behavior and separate plans from local results |
| 5 — Deployment | [Deployment](phase-5-deployment/deployment-guide.md), [proposed CI/CD](phase-5-deployment/ci-cd.md), [release notes](phase-5-deployment/release-notes-template.md) | Define publication and rollback responsibilities |
| 6 — Operations | [Runbook](phase-6-operations/runbook.md), [monitoring](phase-6-operations/monitoring.md), [recovery](phase-6-operations/disaster-recovery.md) | Prepare hosting; infrastructure is not provisioned by these docs |
| Security | [Threat model](security/threat-model.md), [privacy/licensing](security/compliance.md) | Describe trust boundaries and unresolved release decisions |
| User | [Guide](user/user-guide.md), [FAQ](user/faq.md), [troubleshooting](user/troubleshooting.md) | Help customers prepare and download a brief |

## Dates, status and evidence

Historical dates are grounded in Git: `7b3f5ec` (2026-09-22) and `319f047` (2026-10-02). A **baseline date** identifies the source project history, not the time these new documents were written, a test execution date or an approval. Current wizard/documentation work is **Unreleased** and pending review. Add real test dates only alongside actual execution evidence.

**Draft** means review is outstanding. **Proposed** means the deployment, monitoring or integration is guidance requiring implementation and verification. Code is the source for current behavior; mark future work explicitly and update affected documents when behavior changes.

Existing visual records and provenance remain in [frontend-sources.md](frontend-sources.md), `reference-sources` and the preview images. Their historical checks do not certify this wizard iteration.

**PT-BR:** os documentos seguem seis fases do template e explicam este projeto real. Datas antigas são bases verificadas do Git; alterações atuais estão em Unreleased. Pedido baixado não significa pedido recebido pela HTS.
