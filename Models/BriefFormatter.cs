using System.Globalization;
using System.Text;

namespace HowToSoftware.Site.Models;

public static class BriefFormatter
{
    public static string Format(QuoteRequest request, Func<string, string> localize)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(localize);
        var text = new StringBuilder();
        text.AppendLine("HTS — HowTo.Software");
        text.AppendLine(localize("BriefTitle"));

        void Section(string key)
        {
            text.AppendLine();
            text.AppendLine(localize(key));
        }
        void Answer(string key, string? value)
            => text.Append(localize(key)).Append(": ").AppendLine(
                string.IsNullOrWhiteSpace(value) ? localize("WizardNotProvided") : value.Trim());
        string Label(IReadOnlyList<BriefChoice> choices, string? value)
            => BriefOptions.Label(choices, value, localize);
        string Labels(IReadOnlyList<BriefChoice> choices, HashSet<string>? values)
        {
            var selected = choices.Where(choice => values?.Contains(choice.Value) == true)
                .Select(choice => localize(choice.ResourceKey)).ToArray();
            return selected.Length == 0 ? localize("WizardNotProvided") : string.Join(", ", selected);
        }

        Section("WizardStep1");
        Answer("FieldProjectMode", localize(request.ProjectMode == "review" ? "ProjectReview" : "ProjectNew"));
        Answer("FieldService", Label(BriefOptions.Services, request.ServiceType));
        if (request.ProjectMode == "review") Answer("FieldExistingWebsite", request.WebsiteUrl);
        Answer("FieldName", request.FullName);
        Answer("FieldEmail", request.Email);
        Answer("FieldPhone", request.Phone);
        Answer("FieldBusinessName", request.BusinessName);
        Answer("FieldBusinessActivity", request.BusinessActivity);
        Answer("FieldSiteType", Label(BriefOptions.SiteTypes, request.SiteType));

        Section("WizardStep2");
        Answer("FieldDescription", request.Description);
        Answer("FieldAudience", request.Audience);
        Answer("FieldPages", Labels(BriefOptions.Pages, request.Pages));
        if (request.Pages?.Contains("other") == true) Answer("FieldOtherPages", request.OtherPages);
        Answer("FieldFeatures", Labels(BriefOptions.Features, request.Features));

        Section("WizardStep3");
        Answer("FieldStyle", Label(BriefOptions.Styles, request.Style));
        Answer("FieldBrandColors", request.BrandColors);
        Answer("FieldReferences", request.References);
        Answer("FieldContentReadiness", Label(BriefOptions.ContentStatuses, request.ContentReadiness));
        Answer("FieldLanguages", Labels(BriefOptions.Languages, request.Languages));
        if (request.Languages?.Contains("other") == true) Answer("FieldOtherLanguage", request.OtherLanguage);

        Section("WizardStep4");
        Answer("FieldDomainStatus", Label(BriefOptions.Domains, request.DomainStatus));
        Answer("FieldHostingNeed", Label(BriefOptions.Hosting, request.HostingNeed));
        Answer("FieldIntegrations", request.Integrations);
        Answer("FieldBudgetRange", Label(BriefOptions.Budgets, request.BudgetRange));
        if (request.BudgetRange == "defined") Answer("FieldBudgetDetails", request.BudgetDetails);
        Answer("FieldTiming", Label(BriefOptions.Timelines, request.Timing));
        if (request.Timing == "specific")
            Answer("FieldLaunchDate", request.LaunchDate?.ToString("d", CultureInfo.CurrentCulture));
        Answer("FieldExtraNotes", request.ExtraNotes);
        Answer("FieldContactPreference", Label(BriefOptions.ContactMethods, request.ContactPreference));
        text.AppendLine();
        text.AppendLine(localize("BriefNotice"));
        return text.ToString();
    }
}
