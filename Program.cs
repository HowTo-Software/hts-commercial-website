using System.Globalization;
using HowToSoftware.Site.Components;
using HowToSoftware.Site.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddScoped<ProjectBrief>();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var cultures = new[] { new CultureInfo("pt-BR"), new CultureInfo("en"), new CultureInfo("es") };
    options.DefaultRequestCulture = new RequestCulture("pt-BR");
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
    options.FallBackToParentCultures = true;
    options.FallBackToParentUICultures = true;
    options.ApplyCurrentCultureToResponseHeaders = true;
    options.RequestCultureProviders.Clear();
    options.RequestCultureProviders.Add(new CookieRequestCultureProvider());
    options.RequestCultureProviders.Add(new AcceptLanguageHeaderRequestCultureProvider());
});

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRequestLocalization();
app.UseAntiforgery();
app.MapGet("/culture", (HttpContext context, string culture, string? returnUrl) =>
{
    var selectedCulture = culture.Split('-', 2)[0].ToLowerInvariant() switch
    {
        "en" => "en",
        "es" => "es",
        _ => "pt-BR"
    };
    context.Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(selectedCulture)),
        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), IsEssential = true, SameSite = SameSiteMode.Lax, Secure = context.Request.IsHttps });
    var safeReturnUrl = returnUrl is { Length: > 0 } && returnUrl[0] == '/'
        && (returnUrl.Length == 1 || returnUrl[1] is not ('/' or '\\'))
        && !returnUrl.Any(char.IsControl)
        ? returnUrl : "/";
    return Results.LocalRedirect(safeReturnUrl);
}).WithName("SelectCulture");
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();
