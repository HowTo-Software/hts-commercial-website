# Wizard verification — Unreleased

> Scope: separate local evidence for the wizard iteration and the later motion refinement, recorded below. Not a production release or CI certification. Historical baselines remain 2026-09-22 and 2026-10-02.

## Environment

Windows, .NET target `net8.0`, SDK `10.0.400`, Release build, ASP.NET Core Development launch profile, `http://localhost:5093`. Browser checks used Chrome `155.0.8059.39` through the DevTools protocol with an isolated profile and synthetic customer details.

## Executed checks

| Check | Result | Scope |
| --- | --- | --- |
| `dotnet build -c Release` | Passed: zero warnings, zero errors | Final Razor/C# source, including date-input cleanup |
| `node --check wwwroot/js/interactions.js` | Passed | JavaScript syntax |
| Model console harness | 629 assertions passed | Step isolation, allowed/forged options, conditional rules, URL/length limits, complete export and resource coverage in three languages |
| Browser layout matrix | 150 combinations passed | Five steps × three languages × two themes × five widths: 320, 390, 960, 1200 and 1440 px |
| Browser behavior | 23 checks passed | Required fields, invalid email/URLs/references, conditional details, Enter, focus, backward navigation and blocked future steps |
| Date parsing regression | Passed on final Release | Invalid active date blocks Next/progress; correcting, hiding, reactivating or leaving the step does not leave an obsolete parsing error |
| Review and TXT | Passed in PT-BR, EN and ES | Answers and selected labels included; inactive conditional answers omitted; localized UTF-8 download |
| Local HTTP | Passed: status 200 | Home page renders the wizard |

Layout checks measured document width, panel bounds and visible controls. No horizontal overflow or browser runtime errors were found in the matrix. Light/dark desktop and mobile screenshots were also inspected. This does not constitute a screen-reader audit or exhaustive browser certification.

The model harness ran in a temporary directory outside the repository. Browser scripts, reports, screenshots and synthetic downloads are local, ignored artifacts under `artifacts/wizard-qa*`; they are not an installed CI test suite. For future changes, execute the documented [test cases](test-cases.md) and record fresh results.

The date regression exposed an ASP.NET Core 8 lifecycle detail: a disposed input can retain its parsing messages in the edit context. `BriefDateInput` clears its own parser through the protected value setter on disposal and retains the last valid date. The implementation was checked against the [official ASP.NET Core 8 source](https://github.com/dotnet/aspnetcore/blob/v8.0.24/src/Components/Web/src/Forms/InputBase.cs), then verified with invalid, corrected, hidden and recreated date inputs in the browser.

## Remaining release verification

Native browser zoom, other browsers, assistive technology, all unrelated baseline interactions, disconnect/reconnect, staging, load/concurrency, production TLS and error handling have not been certified by this execution. The project still prepares a local file; request delivery, durable persistence, payments and infrastructure are not implemented by the wizard.

**PT-BR:** o build final passou sem erros ou avisos. Foram aprovadas 629 verificações do modelo e 150 combinações de idioma, tema, etapa e largura, além do fluxo real de revisão/download. As datas antigas são bases históricas, não datas fictícias dos testes. Estes resultados são locais e não significam homologação de produção.

## Motion refinement verification — Unreleased

The motion refinement passed a fresh Release build with zero warnings/errors and JavaScript syntax checks for `theme.js` and `interactions.js`. An isolated controller harness passed **37 checks** covering lifecycle, preferences and immediate focused reveals even after entrance delay starts. These are local results, not an installed CI suite, memory profile or accessibility certification.

Fresh browser QA passed **19 behavior assertions** and **48 combinations**: full/paused motion × PT-BR/EN/ES × light/black theme × widths 320, 390, 960 and 1440 px. **192 section-layout checks** found no overflow. The clean final run reported zero console errors, uncaught runtime exceptions or HTTP responses with status 400 or higher. These viewport-width checks are not native browser zoom tests.

| Check | Required result | Recorded state |
| --- | --- | --- |
| Release build and JavaScript syntax | Changed source compiles/parses | Passed locally; zero build warnings/errors, both scripts parse |
| Isolated controller harness | Lifecycle, motion preferences and focused reveal behavior | Passed: 37 checks; corresponding preference/focus behavior also confirmed in the browser |
| Persistent explicit pause | Non-essential motion stops and pause survives navigation/reload | Passed in browser; preference persistence and resume checked |
| Operating-system preference | Reduced motion wins over saved play preference; runtime changes stop motion without hiding content | Passed in browser; dynamic changes plus disabled/localized controls fitting at 320 px in all three languages |
| Informational marquee | One semantic copy; repetition decorative; pause leaves readable static content | Passed in full/paused layout matrix; focused interaction checked |
| Scene visibility | Loops stop offscreen and while the tab is hidden; resume respects preference | Passed with real offscreen and hidden-tab animation-time checks |
| No-script visual fallback | Static page content and original capability labels remain visible; interactive wizard/download is not claimed | Passed in the local no-script visibility check; no concealed/clipped content reported |
| Unavailable preference storage | Motion/theme controls work in the current page without persistence | Passed with simulated storage `SecurityError`; controls still change in-memory state |
| Layout and transforms | Independent float/tilt/reveal, no narrow-width overflow, preview switch keeps scene active | Passed: 48 combinations and 192 section checks; native zoom not repeated |
| Browser interactions | Focus, dialogs, service prefill, wizard Next/Back and localized TXT | Passed locally; complete date-parsing regression remains evidence from the earlier wizard iteration |
| Controller lifecycle | Registration/removal and preferences behave without duplicate wiring in isolated checks | Passed in the 37-check harness; not a browser memory/performance profile |
| Browser error monitoring | No console errors, uncaught exceptions or failed HTTP statuses in executed matrix | Passed for the recorded run; not a production availability guarantee |

The hero float, marquee translation and service-orbit transform changed between real animation samples, while animation time stayed fixed when offscreen or in a genuinely hidden tab. Switching desktop/mobile preview retained continuous-scene state. Eight normal and three reduced-motion mobile screenshots were reviewed locally. These observations establish that the motion and pause controls operated; they are not frame-rate, CPU, memory or production-capacity measurements.

The final motion browser report is local and ignored at `artifacts/motion-qa.json`; browser scripts, screenshots and reports are not a shipped test suite or deployment artifact. Native zoom, broader browser/assistive-technology review, cloud load and complete date-parsing regression on the motion revision were not certified by this run. Keep the earlier wizard evidence scoped to that iteration.

**PT-BR:** build Release sem erros/avisos, sintaxe dos dois scripts e 37 verificações isoladas aprovados. O navegador passou por 19 verificações de comportamento, 48 combinações e 192 verificações de seção, sem overflow ou erros registrados na execução final. Pausa persistida, redução de movimento dinâmica, cenas fora da tela/aba oculta, troca da prévia, foco, diálogos, serviço e navegação/download foram verificados. Conteúdo estático sem JavaScript, controles com armazenamento indisponível e rótulos de movimento reduzido em 320 px nos três idiomas também passaram. Isso não é teste de zoom nativo, perfil de memória ou certificação de produção; a regressão completa de datas permanece na evidência anterior.
