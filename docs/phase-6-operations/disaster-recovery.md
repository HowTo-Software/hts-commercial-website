# Recovery and continuity

> Status: Proposed recovery guidance; targets pending HTS decision. Owner: HTS team. Historical frontend baseline: 2026-10-02. Current edition: Unreleased.

## Recoverable assets and limitations

Keep the Git repository, reviewed release artifacts, platform configuration and locally hosted assets recoverable. Protect secrets in the chosen platform rather than copying them into artifacts. HTS must select retention, storage and access controls for those operational backups.

Customer draft answers are in server memory, not a backup-enabled database. A process or host failure can discard them. The visitor's downloaded TXT resides on that visitor's device and cannot be restored by the site's hosting backup. There are no quote-database recovery procedures or migrations in this iteration.

## Recovery procedure

1. Identify the failed revision/host and preserve sanitized diagnostic evidence.
2. Restore the last verified artifact and matching host configuration.
3. Confirm runtime, TLS, domain routing, assets and Blazor connection support.
4. Re-run the full synthetic wizard review/download smoke test in supported languages/themes.
5. Communicate impact and draft-loss limitations; record cause and corrective work.

Test this procedure in staging before launch. Recovery-time and recovery-point targets require an actual hosting design and business approval; no numerical objective is asserted. If durable request submission is introduced, update this document with storage backup, delivery replay, deletion and reconciliation requirements.

**PT-BR:** recuperar o site não recupera rascunhos em memória. Banco e pedidos persistidos só terão plano próprio quando realmente existirem.
