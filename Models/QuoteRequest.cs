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

    [Required(ErrorMessage = "ValidationRequired"), StringLength(120, ErrorMessage = "ValidationLength120")]
    public string BusinessName { get; set; } = string.Empty;

    [StringLength(300, ErrorMessage = "ValidationLength300")]
    public string BusinessActivity { get; set; } = string.Empty;

    [Required(ErrorMessage = "ValidationRequired")]
    public string SiteType { get; set; } = string.Empty;

    [Required(ErrorMessage = "ValidationRequired"), StringLength(2500, MinimumLength = 15, ErrorMessage = "ValidationDescription")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "ValidationRequired"), StringLength(400, ErrorMessage = "ValidationLength400")]
    public string Audience { get; set; } = string.Empty;

    public HashSet<string> Pages { get; set; } = [];

    [StringLength(400, ErrorMessage = "ValidationLength400")]
    public string OtherPages { get; set; } = string.Empty;

    public HashSet<string> Features { get; set; } = [];

    [Required(ErrorMessage = "ValidationRequired")]
    public string Style { get; set; } = string.Empty;

    [StringLength(160, ErrorMessage = "ValidationLength160")]
    public string BrandColors { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "ValidationLength1000")]
    public string References { get; set; } = string.Empty;

    [Required(ErrorMessage = "ValidationRequired")]
    public string ContentReadiness { get; set; } = string.Empty;

    public HashSet<string> Languages { get; set; } = ["pt-BR"];

    [StringLength(120, ErrorMessage = "ValidationLength120")]
    public string OtherLanguage { get; set; } = string.Empty;

    [StringLength(800, ErrorMessage = "ValidationLength800")]
    public string Integrations { get; set; } = string.Empty;

    [Required(ErrorMessage = "ValidationRequired")]
    public string DomainStatus { get; set; } = string.Empty;

    [Required(ErrorMessage = "ValidationRequired")]
    public string HostingNeed { get; set; } = string.Empty;

    [Required(ErrorMessage = "ValidationRequired")]
    public string BudgetRange { get; set; } = string.Empty;

    [StringLength(160, ErrorMessage = "ValidationLength160")]
    public string BudgetDetails { get; set; } = string.Empty;

    [Required(ErrorMessage = "ValidationRequired")]
    public string Timing { get; set; } = string.Empty;

    public DateTime? LaunchDate { get; set; }

    [StringLength(1000, ErrorMessage = "ValidationLength1000")]
    public string ExtraNotes { get; set; } = string.Empty;

    [Required(ErrorMessage = "ValidationRequired")]
    public string ContactPreference { get; set; } = "email";

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        return BriefValidation.Validate(this);
    }
}
