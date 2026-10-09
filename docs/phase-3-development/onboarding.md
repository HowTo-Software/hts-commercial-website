# Developer onboarding

> Status: Draft, pending HTS review. Owner: HTS team. Historical frontend baseline: 2026-10-02. Current edition: Unreleased.

## Prepare and run

Use the .NET 8 SDK and a C#/Razor editor. Work in the repository containing `HowToSoftware.Site.csproj`. No Node/npm, database or account-provider credentials are needed for the current site.

```powershell
dotnet --info
git status --short --branch
dotnet restore
dotnet build -c Release
dotnet run --no-launch-profile --urls http://localhost:5093
```

Open <http://localhost:5093>. If the port is occupied, identify the process before stopping it or choose another local port. The current app settings contain logging and host options, not request-delivery secrets. Do not add personal configuration to tracked files.

## First change

Read [architecture](../phase-2-design/architecture.md), `Home.razor`, `QuoteForm.razor`, the two model classes and localization resources. Services and projects can change the shared brief, so a wizard edit must preserve that path. Update PT-BR, EN and ES keys together and check both themes.

There is no baseline automated test project; `dotnet test` alone is not evidence of wizard coverage. Use the [test matrix](../phase-4-testing/test-cases.md), actual browser results and Release build output. Do not claim the old preview images certify current behavior.

Ask the HTS team about product decisions, the request-delivery channel or production hosting. Do not invent keys or treat the download-only workflow as a missing local configuration secret.

**PT-BR:** basta o SDK .NET 8 para revisar o frontend; não são exigidas chaves de envio inexistentes.
