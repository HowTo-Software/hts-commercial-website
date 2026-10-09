namespace HowToSoftware.Site.Models;

public sealed record BriefChoice(string Value, string ResourceKey);

public static class BriefOptions
{
    public static IReadOnlyList<BriefChoice> Services { get; } = Choices(
        ("website", "ServiceWebDev"), ("business", "ServiceBusinessSites"),
        ("landing", "ServiceLanding"), ("custom", "ServiceCustom"),
        ("maintenance", "ServiceMaintenance"), ("backend", "ServiceBackend"),
        ("hosting", "ServiceHosting"), ("consulting", "ServiceConsulting"));

    public static IReadOnlyList<BriefChoice> SiteTypes { get; } = Choices(
        ("business", "SiteTypeBusiness"), ("landing", "SiteTypeLanding"),
        ("store", "SiteTypeStore"), ("portfolio", "SiteTypePortfolio"),
        ("blog", "SiteTypeBlog"), ("platform", "SiteTypePlatform"), ("unsure", "ChoiceNeedGuidance"));

    public static IReadOnlyList<BriefChoice> Pages { get; } = Choices(
        ("home", "PageHome"), ("about", "PageAbout"), ("services", "PageServices"),
        ("products", "PageProducts"), ("portfolio", "PagePortfolio"), ("blog", "PageBlog"),
        ("faq", "PageFaq"), ("contact", "PageContact"), ("other", "ChoiceOther"));

    public static IReadOnlyList<BriefChoice> Features { get; } = Choices(
        ("contact-form", "FeatureContactForm"), ("whatsapp", "FeatureWhatsApp"),
        ("booking", "FeatureBooking"), ("store", "FeatureStore"), ("payments", "FeaturePayments"),
        ("login", "FeatureLogin"), ("blog", "FeatureBlog"), ("cms", "FeatureCms"),
        ("search", "FeatureSearch"), ("analytics", "FeatureAnalytics"));

    public static IReadOnlyList<BriefChoice> Styles { get; } = Choices(
        ("minimal", "StyleMinimal"), ("bold", "StyleBold"), ("elegant", "StyleElegant"),
        ("corporate", "StyleCorporate"), ("creative", "StyleCreative"), ("unsure", "ChoiceNeedGuidance"));

    public static IReadOnlyList<BriefChoice> ContentStatuses { get; } = Choices(
        ("ready", "ContentReady"), ("partial", "ContentPartial"), ("help", "ContentNeedHelp"));

    public static IReadOnlyList<BriefChoice> Languages { get; } = Choices(
        ("pt-BR", "LanguagePortuguese"), ("en", "LanguageEnglish"), ("es", "LanguageSpanish"),
        ("other", "ChoiceOther"));

    public static IReadOnlyList<BriefChoice> Domains { get; } = Choices(
        ("have", "DomainHave"), ("need", "DomainNeed"), ("unsure", "ChoiceNeedGuidance"));

    public static IReadOnlyList<BriefChoice> Hosting { get; } = Choices(
        ("hts", "HostingHts"), ("existing", "HostingExisting"), ("recommendation", "HostingRecommendation"));

    public static IReadOnlyList<BriefChoice> Budgets { get; } = Choices(
        ("defined", "BudgetDefined"), ("advice", "BudgetAdvice"));

    public static IReadOnlyList<BriefChoice> Timelines { get; } = Choices(
        ("flexible", "TimingFlexible"), ("one-month", "TimingOneMonth"),
        ("one-three-months", "TimingOneThreeMonths"), ("specific", "TimingSpecific"));

    public static IReadOnlyList<BriefChoice> ContactMethods { get; } = Choices(
        ("email", "ContactEmail"), ("phone", "ContactPhone"), ("whatsapp", "ContactWhatsApp"));

    public static string Label(IReadOnlyList<BriefChoice> choices, string? value, Func<string, string> localize)
    {
        var choice = choices.FirstOrDefault(item => item.Value == value);
        return localize(choice?.ResourceKey ?? "WizardNotProvided");
    }

    private static IReadOnlyList<BriefChoice> Choices(params (string Value, string Key)[] choices)
        => Array.AsReadOnly(choices.Select(item => new BriefChoice(item.Value, item.Key)).ToArray());
}
