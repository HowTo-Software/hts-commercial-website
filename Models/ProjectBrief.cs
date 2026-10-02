namespace HowToSoftware.Site.Models;

// The draft belongs to the current Blazor circuit; personal data is not persisted.
public sealed class ProjectBrief
{
    public QuoteRequest Request { get; } = new();
    public event Action? Changed;
    public void SelectService(string service)
    {
        Request.ServiceType = service;
        if (service == "maintenance") Request.ProjectMode = "review";
        Changed?.Invoke();
    }
}
