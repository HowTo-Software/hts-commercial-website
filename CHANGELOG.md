# Changelog

> Status: Draft record; HTS review pending. Owner: HTS team. Dates below are verified Git history, not release approvals. Current edition: Unreleased.

## Unreleased

### Added / Adicionado

- Guided custom-website request with five stages covering contact/project, scope, visual direction, technical planning and review/download.
- Project-specific lifecycle documentation based on the local `best-document` structure, including requirements, design, development, testing, deployment, operations, security and user guidance.
- English-first documentation with PT-BR guidance and a Portuguese README.
- Motion-refinement scope: subtle hero float/decorative accents, capabilities marquee, service-art orbits, staged entrances and wizard-step transitions, with no additional animation dependency. / Escopo do refinamento: hero suave, faixa de capacidades, órbitas decorativas, entradas graduais e transições entre etapas.
- Persistent animation pause/resume, operating-system reduced-motion priority, offscreen/hidden-tab suspension and marquee hover/focus pause. Repetitions remain decorative; static content remains available. / Pausa persistida, prioridade à redução de movimento, suspensão fora da área visível/em abas ocultas e pausa da faixa no hover/foco.

### Local verification / Verificação local

- Motion revision: Release build with zero warnings/errors; both JavaScript files parse; 37 isolated controller checks, 19 browser behavior assertions and 48 combinations/192 section-layout checks passed. Static no-script content and storage-unavailable controls were checked separately. Native browser zoom, memory profiling and production capacity were not certified. See [verification](docs/phase-4-testing/verification.md). / Movimento: build sem erros/avisos, sintaxe dos scripts, 37 verificações isoladas, 19 verificações de comportamento e 48 combinações/192 checks de seção aprovados, com fallback estático e controles sem armazenamento; sem certificação de zoom nativo, memória ou capacidade de produção.

### Preserved / Preservado

- Blazor/.NET 8, current visual language, PT-BR/EN/ES, light/black themes, service-prefill behavior and optional existing-site URL validation.
- Local UTF-8 brief download. No API delivery, database persistence, email transmission or payment workflow is introduced by the documentation.
- Existing preview files, source records and asset attribution.

### Fixed / Corrigido

- Preview-mode and project-filter controls render explicit `aria-pressed` values. / Controles da prévia e filtros informam explicitamente seu estado acessível.
- The hero observes a stable parent so changing desktop/mobile preview does not remove viewport motion state. / A cena do hero usa um pai estável para manter o estado de movimento ao trocar a prévia.
- Focusing revealed content bypasses a pending entrance delay immediately. / Foco em conteúdo revelado cancela a espera da animação de entrada.

## Frontend baseline — 2026-10-02

Verified commit: `319f047` — `feat: reformula site HTS com temas, idiomas e serviços completos`.

- Editorial frontend with light/black themes, localized resources, services, project concepts and local text briefing.
- Source/attribution record in [frontend-sources.md](docs/frontend-sources.md).

## Initial project baseline — 2026-09-22

Verified commit: `7b3f5ec` — `Initial HTS commercial Blazor website`.

- Initial commercial HTS website using Blazor and .NET 8.

These baselines are historical checkpoints. No version tags, production release dates, customer deliveries or reviewer approvals are inferred from the commit dates.

**PT-BR:** as datas antigas identificam eventos reais do Git. O assistente e esta documentação permanecem em Unreleased até publicação efetiva.
