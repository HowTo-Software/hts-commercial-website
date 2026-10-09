# System and wizard diagrams

> Status: Draft, pending HTS review. Owner: HTS team. Historical frontend baseline: 2026-10-02. Current edition: Unreleased.

## Request preparation

```mermaid
sequenceDiagram
    actor Visitor
    participant Page as Blazor page
    participant Draft as Scoped ProjectBrief
    participant Browser as Download helper
    Visitor->>Page: Select service
    Page->>Draft: Update service / existing-project mode
    Visitor->>Page: Answer steps 1–4
    Page->>Draft: Store circuit state and validate
    Visitor->>Page: Review and edit
    Visitor->>Page: Download brief
    Page->>Draft: Revalidate complete request
    Page->>Browser: Pass localized text through JS interop
    Browser-->>Visitor: UTF-8 TXT file on device
    Note over Page,Draft: No database or request-delivery API
```

## Navigation state

```mermaid
stateDiagram-v2
    [*] --> ContactProject
    ContactProject --> GoalsScope: Valid Next
    GoalsScope --> VisualDirection: Valid Next
    VisualDirection --> TechnicalPlanning: Valid Next
    TechnicalPlanning --> Review: Valid Next
    Review --> ContactProject: Edit project
    Review --> GoalsScope: Edit scope
    Review --> VisualDirection: Edit style
    Review --> TechnicalPlanning: Edit planning
    Review --> Downloaded: Complete validation and export
    Downloaded --> Review: Further edits
```

Back navigation between adjacent steps also preserves the draft. A circuit expiration or reload can reset the flow. These diagrams describe preparation, not a payment, acceptance or email-submission sequence. See [architecture](architecture.md) for deployable units and [deployment guide](../phase-5-deployment/deployment-guide.md) for proposed hosting.

**PT-BR:** o diagrama termina em arquivo local; não existe seta de envio para a equipe HTS.
