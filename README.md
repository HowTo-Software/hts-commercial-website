# HTS — HowTo.Software

A commercial website for HTS — HowTo.Software, built with **ASP.NET Core, Blazor Web App, Razor components, and .NET 8**. Its dark visual system, orange accents, responsive layout, and reusable sections present the company's digital services.

![HTS website desktop preview](docs/preview-desktop.png)

## Features

- Responsive header, hero, services, differentiators, project cards, quote form, and footer.
- English and Brazilian Portuguese localization through .NET resource files. The first visit follows the browser's `Accept-Language` preference; `/culture` can save an explicit choice in a cookie.
- Server-interactive quote form with field validation and clear on-page feedback.
- Local image and SVG assets. No frontend framework or JavaScript dependency is needed beyond Blazor's runtime.
- Small hover and navigation effects that respect reduced-motion preferences in the header.

## Run locally

Install the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0), then run:

```powershell
dotnet restore
dotnet run --no-launch-profile --urls http://localhost:5093
```

Open <http://localhost:5093>. You can choose another available port. For a Portuguese preview, set your browser language to `pt-BR`, or visit `/culture?culture=pt-BR&returnUrl=%2F`. Use `culture=en` to switch back.

The main page is assembled in `Components/Pages/Home.razor` from components in `Components/Shared`. Styles live in `wwwroot/css/app.css`, images in `wwwroot/images`, and translations in `Resources/AppStrings.resx` and `Resources/AppStrings.pt-BR.resx`.

## Current scope

**Validation only:** the app does not create, store, email, or forward quote requests. An orders API, file attachments, and a new-project versus revision choice are future work. The Blog link leads to a “coming soon” note in the footer; social links are placeholders. Project cards use the bundled titles and images, which can be replaced with verified case studies.

Browser zoom stays under the visitor's control. The layout adapts to viewport width and uses `width=device-width, initial-scale=1`.

## Português (Brasil)

Site institucional e comercial da **HTS — HowTo.Software**, desenvolvido com **ASP.NET Core, Blazor Web App, componentes Razor e .NET 8**. O visual combina fundo escuro, detalhes em laranja, seções reutilizáveis e layout responsivo.

O site inclui cabeçalho, apresentação, serviços, diferenciais, cards de projetos, formulário de orçamento e rodapé. Os textos usam recursos de localização do .NET em inglês e português brasileiro. Na primeira visita, o idioma acompanha a preferência do navegador; o endpoint `/culture` permite salvar uma escolha em cookie.

Para executar, instale o SDK do .NET 8 e rode `dotnet restore` seguido de `dotnet run --no-launch-profile --urls http://localhost:5093`. Acesse <http://localhost:5093>. Os componentes estão em `Components/Shared`, os estilos em `wwwroot/css/app.css`, as imagens em `wwwroot/images` e as traduções em `Resources`.

**Escopo atual:** o formulário valida os campos, mas a aplicação não cria, armazena, envia por e-mail ou encaminha pedidos de orçamento. A integração com o backend, os anexos e a opção de projeto novo ou revisão ficam para uma próxima etapa. O Blog indica “em breve” no rodapé, e os links sociais são provisórios. Os cards de projetos usam títulos e imagens incluídos no frontend e podem ser substituídos por casos reais verificados.
