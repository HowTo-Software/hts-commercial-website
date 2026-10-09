# Git workflow

> Status: Draft recommended workflow; repository policy confirmation pending. Owner: HTS team. Historical frontend baseline: 2026-10-02. Current edition: Unreleased.

Inspect the current branch, remote and uncommitted files before starting. Preserve existing work. Fetch and compare with the target base before synchronizing; do not reset, force-push or discard files to resolve ordinary updates.

Work in a focused feature/fix/docs branch when appropriate to the owner's instructions. Suggested names use English, such as `feature/website-request-wizard`. The repository owner determines whether this task should be committed, pushed or merged; a documentation convention does not create authorization for publication.

## Reviewable changes

A PR description should lead with the resulting customer behavior, provide English and PT-BR, explain the server-circuit/download limitation and list actual validation evidence. Describe related model/resource/CSS changes together. Avoid unsupported “production ready,” historic release dates or imaginary test counts.

Keep credentials, `bin`, `obj`, browser profiles and local artifacts out of commits. Review `git diff --check`, the changed files and untracked files before staging. Do not remove source/attribution records in a frontend cleanup.

## Releases

This baseline has historical commits, not documented semantic-version releases. Keep current changes in `Unreleased` until a real publication occurs. A tag, release date and reviewer names must come from actual events. Branch protection and mandatory CI are proposals until configured on the host.

**PT-BR:** Git registra fatos; não invente aprovação nem data de release. Publicação segue a autorização do responsável.
