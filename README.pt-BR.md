# HTS — HowTo.Software

> Estado: documentação, assistente de pedido e refinamento de movimento em Unreleased; revisão da equipe HTS pendente. Responsável: equipe HTS. Base histórica: 22/09/2026; base do frontend: 02/10/2026.

Site comercial da HTS para criação de sites personalizados e serviços digitais, desenvolvido em **Blazor Web App, ASP.NET Core e .NET 8**. O frontend mantém a identidade editorial, os detalhes em laranja, os temas claro e black e a navegação em PT-BR, EN e ES.

[Documentação principal em inglês](README.md) · [Índice de documentos](docs/README.md) · [Histórico](CHANGELOG.md) · [Fontes do frontend](docs/frontend-sources.md)

## Executar

Com o SDK .NET 8 instalado, execute nesta pasta:

```powershell
dotnet restore
dotnet build -c Release
dotnet run --no-launch-profile --urls http://localhost:5093
```

Acesse <http://localhost:5093>. Não são necessários Node, React ou npm. Imagens e fonte Manrope são servidas localmente.

## Pedido de criação de site

O briefing passa a ser um assistente de cinco etapas, conforme a orientação de Hank para perguntar ao cliente como ele deseja seu site:

1. **Contato e projeto:** dados de contato, negócio, serviço, tipo de site, projeto novo ou existente e endereço opcional do site atual.
2. **Objetivos e escopo:** resultado esperado, público, páginas e funcionalidades.
3. **Identidade visual:** estilo, cores, referências, materiais disponíveis e idiomas do site.
4. **Tecnologia e planejamento:** integrações, domínio/hospedagem, faixa de orçamento e prazo desejado.
5. **Revisão:** conferir respostas, voltar para corrigir uma etapa e baixar o TXT em UTF-8.

A seleção de um serviço continua preenchendo o briefing. Manutenção abre o modo de projeto existente; quando informado, o endereço deve usar HTTP ou HTTPS.

**O arquivo é baixado no dispositivo do cliente. O pedido ainda não é enviado à HTS, salvo em banco nem encaminhado por e-mail.** Não há login ou pagamento neste projeto. O rascunho pertence ao circuito Blazor e pode ser perdido em recarga, reinício ou expiração da conexão. Não inclua senhas, chaves ou dados sensíveis no briefing.

## Movimento profissional — Unreleased

O refinamento amplia a animação existente com flutuação suave da prévia principal, detalhes decorativos discretos, faixa de capacidades em movimento, órbitas lentas na arte dos serviços, entradas graduais de seções/cards e transições curtas entre etapas do briefing. Textos, campos e ações devem continuar estáveis; não há nova biblioteca de animação ou dependência de CDN.

Controles visíveis junto à prévia principal e na faixa de capacidades pausam/retomam as animações e compartilham a preferência `hts-motion`. A redução de movimento escolhida no sistema operacional tem prioridade. Cenas contínuas param fora da área visível ou quando a aba está oculta. A faixa também pausa no hover/foco; a cópia visual repetida é ignorada por leitores de tela, e a versão pausada/reduzida apresenta o conteúdo original em uma disposição estática com quebra de linha.

Sem JavaScript, o fallback mantém o conteúdo da página e os textos da faixa visíveis; ele não substitui o Blazor interativo. Navegação do assistente, retorno de validação e download precisam de JavaScript e conexão ativa com o servidor.

O refinamento passou pelo build Release, 37 verificações isoladas do controlador, 19 verificações de comportamento no navegador e 48 combinações, com 192 verificações de layout das seções. O [registro de verificação](docs/phase-4-testing/verification.md) detalha pausa, redução de movimento, cenas fora da tela/aba oculta, fallback estático e interações. Zoom nativo, perfil de memória e capacidade de produção não estão homologados; os resultados do assistente anterior permanecem separados.

## Documentação e datas

A estrutura de `best-document` foi adaptada ao projeto em seis fases: requisitos, arquitetura, desenvolvimento, testes, implantação e operação, além de segurança e guia do usuário. Os textos distinguem funcionalidades existentes, o trabalho ainda não publicado e orientações futuras.

As datas históricas são verificadas no Git: criação em **22/09/2026** (`7b3f5ec`) e reformulação do frontend em **02/10/2026** (`319f047`). Esta atualização permanece em **Unreleased**, sem atribuir uma data antiga fictícia ou aprovação inexistente.

Antes de publicar, execute o build, a matriz de [testes](docs/phase-4-testing/test-cases.md), revise hospedagem e conexão Blazor e defina o canal real de recebimento. O [CI/CD](docs/phase-5-deployment/ci-cd.md) no GitHub Actions valida cada PR e publica `main` via runner self-hosted `192.168.1.210` no host Docker `192.168.1.206` (`/opt/hts-commercial-website`). O monitoramento continua documentado como proposta.

O assistente, antes do refinamento de movimento, passou pelo build Release, 629 verificações do modelo e 150 combinações de layout no navegador. O movimento tem resultados novos e separados no [registro de verificação local](docs/phase-4-testing/verification.md). Nenhuma dessas execuções representa homologação de produção.

## Prévia e atribuição

[Desktop](docs/preview-desktop.png) · [Mobile](docs/preview-mobile.png) · [Página completa](docs/preview-desktop-full.png) · [Black](docs/preview-black.png) · [Black mobile](docs/preview-black-mobile.png) · [Espanhol](docs/preview-es.png)

As imagens registram a base visual anterior ao assistente. Skiper UI mantém atribuição no rodapé. A referência do OriginKit foi implementada de forma independente, conforme [frontend-sources.md](docs/frontend-sources.md). A licença da fonte Manrope está em `wwwroot/fonts/OFL.txt`. A licença geral do projeto ainda precisa de uma decisão da HTS; o arquivo de escolha de licença do template não foi copiado como uma licença concedida.
