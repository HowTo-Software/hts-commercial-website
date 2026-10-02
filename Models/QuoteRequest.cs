using System.ComponentModel.DataAnnotations;

namespace HowToSoftware.Site.Models;

public sealed class QuoteRequest : IValidatableObject
{
    [Required(ErrorMessage = "ValidationRequired"), StringLength(100, ErrorMessage = "ValidationNameLength")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "ValidationRequired"), EmailAddress(ErrorMessage = "ValidationEmail"), StringLength(160, ErrorMessage = "ValidationEmailLength")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "ValidationRequired"), StringLength(40, ErrorMessage = "ValidationPhoneLength")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "ValidationRequired")]
    public string ServiceType { get; set; } = string.Empty;

    public string ProjectMode { get; set; } = "new";

    public string WebsiteUrl { get; set; } = string.Empty;

    [Required(ErrorMessage = "ValidationRequired"), StringLength(2500, MinimumLength = 15, ErrorMessage = "ValidationDescription")]
    public string Description { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (ProjectMode != "review" || string.IsNullOrWhiteSpace(WebsiteUrl)) yield break;

        var address = WebsiteUrl.Trim();
        if (address.Length > 2048)
            yield return new ValidationResult("ValidationWebsiteLength", new[] { nameof(WebsiteUrl) });
        else if (!Uri.TryCreate(address, UriKind.Absolute, out var uri) ||
                 (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
            yield return new ValidationResult("ValidationWebsite", new[] { nameof(WebsiteUrl) });
    }
}
