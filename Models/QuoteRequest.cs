using System.ComponentModel.DataAnnotations;

namespace HowToSoftware.Site.Models;

public sealed class QuoteRequest
{
    [Required, StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(160)]
    public string Email { get; set; } = string.Empty;

    [Required, StringLength(40)]
    public string Phone { get; set; } = string.Empty;

    [Required]
    public string ServiceType { get; set; } = string.Empty;

    [Required, StringLength(2500, MinimumLength = 15)]
    public string Description { get; set; } = string.Empty;
}
