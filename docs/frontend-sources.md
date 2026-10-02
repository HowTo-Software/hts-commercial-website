# Frontend HTS — fontes e adaptações

Reformulação realizada em 2 de outubro de 2026, mantendo Blazor Web App e .NET 8.

## Skiper UI: código coletado e adaptado

Os componentes gratuitos foram obtidos pelos endpoints públicos do registry oficial:

| Componente | Fonte | Adaptação na HTS |
| --- | --- | --- |
| Text roll navigation / skiper58 | [Documentação](https://skiper-ui.com/v1/skiper58) · [Registry JSON](https://skiper-ui.com/r/skiper58.json) | RollingText.razor: duas camadas de letras, deslocamento de 100% e atraso por caractere. Anima com CSS no hover e no foco pelo teclado. |
| CssLink / skiper40 | [Documentação](https://skiper-ui.com/v1/skiper40) · [Registry JSON](https://skiper-ui.com/r/skiper40.json) | .animated-link em app.css: sublinhado com origem direcional e movimento discreto da seta. |

O código original obtido está em docs/reference-sources/skiper58.tsx e skiper40.tsx, para consulta. Esses arquivos não são executados nem enviados ao navegador. As implementações foram portadas para Razor/CSS, sem adicionar React, Next.js, Tailwind ou Framer Motion ao projeto.

A documentação do Skiper UI permite uso e modificação em projetos pessoais e comerciais e exige atribuição na versão gratuita. O rodapé inclui um link visível para Skiper UI. O CssLink também credita a referência original de [Cursor](https://cursor.com/).

## OriginKit: referência pública e implementação independente

- Referência específica: [Mask Text Reveal](https://www.originkit.dev/components/mask-text-reveal).
- Consulta ao catálogo e ao [guia de componentes](https://www.originkit.dev/docs/components).
- A cópia/instalação do código exige autenticação; não havia uma conta ou chave OriginKit disponível nesta sessão. Nenhum código restrito, componente pago ou endpoint autenticado foi obtido.
- MaskReveal.razor é uma implementação independente a partir do comportamento e da API documentados publicamente, e não uma cópia do código do OriginKit.
- O componente admite revelação por baixo, cima, esquerda, direita, centro horizontal e centro vertical. CSS faz a máscara; IntersectionObserver dispara a entrada apenas quando o título chega à área visível.
- A máscara anima um elemento interno. O observador acompanha a caixa externa para evitar que uma máscara totalmente fechada impeça o próprio disparo.

Para substituir a implementação por uma adaptação do código original, será necessário obter o componente por uma conta autorizada no OriginKit. [Licenciamento do OriginKit](https://www.originkit.dev/docs/licensing).

## Interações próprias da HTS

- Prévia do hero alterna entre formato desktop e mobile.
- Projetos têm filtros e diálogos nativos, com fechamento por botão, clique fora e Escape.
- A seleção de um serviço, ou do CTA de um conceito, preenche o briefing no mesmo circuito Blazor.
- Backend e hospedagem têm serviços próprios. A manutenção abrange sites criados pela HTS e por outras equipes, e selecioná-la abre o briefing para um projeto existente.
- Projetos existentes podem incluir o endereço do site, com validação de HTTP/HTTPS e presença no TXT. O campo é opcional e não aparece ao começar do zero.
- Briefing tem escolha de projeto novo ou melhoria, mensagens localizadas e download de TXT em UTF-8. Não há envio, gravação em banco ou e-mail.
- Navegação indica a seção atual e o progresso de leitura.
- Menu mobile usa details, fecha após navegar, ao clicar fora e com Escape.
- Cabeçalho oferece PT-BR, EN e ES. Traduções incluem conteúdo, mensagens de validação, metadados e download do briefing. A escolha fica em cookie e a troca preserva a seção atual.
- Tema claro e black com escolha salva no navegador. theme.js aplica a preferência no head, antes do CSS, e atualiza o rótulo do botão no idioma selecionado.
- Tilt discreto apenas na prévia principal, somente com ponteiro fino.
- As entradas respeitam prefers-reduced-motion. O conteúdo permanece visível quando o JavaScript não carrega.
- interactions.js também observa os componentes inseridos por atualizações do Blazor e remove observações dos elementos desmontados.

## Assets e fontes

As imagens já incluídas no projeto foram reaproveitadas. Os projetos são apresentados como conceitos demonstrativos, sem alegações de clientes ou resultados reais.

Fonte Manrope variável hospedada localmente, obtida do [repositório oficial Google Fonts](https://github.com/google/fonts/tree/main/ofl/manrope). Licença SIL Open Font License em wwwroot/fonts/OFL.txt. Nenhum CDN é necessário em tempo de execução.

## Validação

Build .NET e revisão em Chrome headless: português/inglês/espanhol, temas claro e black, formatos responsivos, persistência das preferências, filtros, diálogos, teclado, serviços expansíveis, seleção de serviço, mensagens de validação, download do briefing, redução de movimento e visibilidade das animações. Manutenção, backend e hospedagem foram verificados nos três idiomas, incluindo o preenchimento do briefing, endereço opcional, validação de URL e conteúdo dos arquivos baixados. Foram revisadas 30 combinações de tema, idioma e largura, de 320 a 1440 px. As prévias estão em docs/preview-desktop.png, docs/preview-mobile.png, docs/preview-black.png, docs/preview-black-mobile.png e docs/preview-es.png.
