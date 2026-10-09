using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HowToSoftware.Site.Models;

public static class BriefValidation
{
    private static readonly string[][] StepFields =
    [
        [nameof(QuoteRequest.FullName), nameof(QuoteRequest.Email), nameof(QuoteRequest.Phone),
         nameof(QuoteRequest.BusinessName), nameof(QuoteRequest.BusinessActivity),
         nameof(QuoteRequest.ServiceType), nameof(QuoteRequest.ProjectMode), nameof(QuoteRequest.WebsiteUrl),
         nameof(QuoteRequest.SiteType)],
        [nameof(QuoteRequest.Description), nameof(QuoteRequest.Audience),
         nameof(QuoteRequest.Pages), nameof(QuoteRequest.OtherPages), nameof(QuoteRequest.Features)],
        [nameof(QuoteRequest.Style), nameof(QuoteRequest.BrandColors), nameof(QuoteRequest.References),
         nameof(QuoteRequest.ContentReadiness), nameof(QuoteRequest.Languages), nameof(QuoteRequest.OtherLanguage)],
        [nameof(QuoteRequest.Integrations), nameof(QuoteRequest.DomainStatus), nameof(QuoteRequest.HostingNeed),
         nameof(QuoteRequest.BudgetRange), nameof(QuoteRequest.BudgetDetails), nameof(QuoteRequest.Timing),
         nameof(QuoteRequest.LaunchDate), nameof(QuoteRequest.ExtraNotes), nameof(QuoteRequest.ContactPreference)]
    ];

    public static IReadOnlyList<ValidationResult> ValidateStep(QuoteRequest request, int step)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentOutOfRangeException.ThrowIfNegative(step);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(step, StepFields.Length - 1);

        var results = new List<ValidationResult>();
        // Exclude hidden conditional fields from property validation.
        foreach (var field in StepFields[step].Where(field => Applies(request, field)))
        {
            var context = new ValidationContext(request) { MemberName = field };
            var value = typeof(QuoteRequest).GetProperty(field)!.GetValue(request);
            Validator.TryValidateProperty(value, context, results);
        }

        switch (step)
        {
            case 0:
                Choice(request.ServiceType, BriefOptions.Services, nameof(request.ServiceType), results);
                Choice(request.SiteType, BriefOptions.SiteTypes, nameof(request.SiteType), results);
                if (request.ProjectMode is not ("new" or "review"))
                    Error("ValidationSelection", nameof(request.ProjectMode), results);
                if (request.ProjectMode == "review" && !string.IsNullOrWhiteSpace(request.WebsiteUrl))
                {
                    if (request.WebsiteUrl.Length > 2048)
                        Error("ValidationWebsiteLength", nameof(request.WebsiteUrl), results);
                    else if (!IsPublicWebAddress(request.WebsiteUrl.Trim()))
                        Error("ValidationWebsite", nameof(request.WebsiteUrl), results);
                }
                break;
            case 1:
                Selection(request.Pages, BriefOptions.Pages, nameof(request.Pages), true, results);
                Selection(request.Features, BriefOptions.Features, nameof(request.Features), false, results);
                if (request.Pages?.Contains("other") == true)
                    Required(request.OtherPages, nameof(request.OtherPages), results);
                break;
            case 2:
                Choice(request.Style, BriefOptions.Styles, nameof(request.Style), results);
                Choice(request.ContentReadiness, BriefOptions.ContentStatuses, nameof(request.ContentReadiness), results);
                Selection(request.Languages, BriefOptions.Languages, nameof(request.Languages), true, results);
                if (request.Languages?.Contains("other") == true)
                    Required(request.OtherLanguage, nameof(request.OtherLanguage), results);
                var references = (request.References ?? string.Empty).Split(
                    ['\r', '\n'], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                if (references.Length > 5 || references.Any(address => !IsPublicWebAddress(address)))
                    Error("ValidationReferences", nameof(request.References), results);
                break;
            case 3:
                Choice(request.DomainStatus, BriefOptions.Domains, nameof(request.DomainStatus), results);
                Choice(request.HostingNeed, BriefOptions.Hosting, nameof(request.HostingNeed), results);
                Choice(request.BudgetRange, BriefOptions.Budgets, nameof(request.BudgetRange), results);
                Choice(request.Timing, BriefOptions.Timelines, nameof(request.Timing), results);
                Choice(request.ContactPreference, BriefOptions.ContactMethods, nameof(request.ContactPreference), results);
                if (request.BudgetRange == "defined")
                    Required(request.BudgetDetails, nameof(request.BudgetDetails), results);
                if (request.Timing == "specific" && request.LaunchDate is null)
                    Error("ValidationLaunchDate", nameof(request.LaunchDate), results);
                break;
        }
        return results;
    }

    public static IReadOnlyList<ValidationResult> Validate(QuoteRequest request)
        => Enumerable.Range(0, StepFields.Length).SelectMany(step => ValidateStep(request, step)).ToArray();

    public static int StepForField(string field)
    {
        for (var step = 0; step < StepFields.Length; step++)
            if (StepFields[step].Contains(field, StringComparer.Ordinal)) return step;
        return 0;
    }

    private static bool Applies(QuoteRequest request, string field) => field switch
    {
        nameof(QuoteRequest.WebsiteUrl) => request.ProjectMode == "review",
        nameof(QuoteRequest.OtherPages) => request.Pages?.Contains("other") == true,
        nameof(QuoteRequest.OtherLanguage) => request.Languages?.Contains("other") == true,
        nameof(QuoteRequest.BudgetDetails) => request.BudgetRange == "defined",
        nameof(QuoteRequest.LaunchDate) => request.Timing == "specific",
        _ => true
    };

    private static bool IsPublicWebAddress(string value)
        => Uri.TryCreate(value, UriKind.Absolute, out var uri)
           && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
           && !string.IsNullOrWhiteSpace(uri.Host) && string.IsNullOrEmpty(uri.UserInfo);

    private static void Required(string? value, string field, List<ValidationResult> results)
    {
        if (string.IsNullOrWhiteSpace(value)) Error("ValidationRequired", field, results);
    }

    private static void Choice(string? value, IReadOnlyList<BriefChoice> choices, string field, List<ValidationResult> results)
    {
        // RequiredAttribute reports empty answers; this also rejects forged IDs.
        if (!string.IsNullOrWhiteSpace(value) && !choices.Any(choice => choice.Value == value))
            Error("ValidationSelection", field, results);
    }

    private static void Selection(HashSet<string>? values, IReadOnlyList<BriefChoice> choices,
        string field, bool required, List<ValidationResult> results)
    {
        if (required && (values is null || values.Count == 0))
            Error("ValidationChooseAtLeastOne", field, results);
        if (values is not null && values.Any(value => !choices.Any(choice => choice.Value == value)))
            Error("ValidationSelection", field, results);
    }

    private static void Error(string key, string field, List<ValidationResult> results)
        => results.Add(new ValidationResult(key, [field]));
}
