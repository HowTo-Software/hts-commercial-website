namespace HowToSoftware.Site.Models;

public sealed record PortfolioProject(
    string Index, string Title, string Brand, string Url, string? Image, string? Alt, string Monogram,
    string Type, string Description, string Headline, string Status, string Theme, string Category, string Service)
{
    public string Domain => new Uri(Url, UriKind.Absolute).Host;
    public string DialogId => $"project-{Index}";
}
