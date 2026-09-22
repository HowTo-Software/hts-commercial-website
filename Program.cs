using System.Globalization;
using HowToSoftware.Site.Components;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var cultures = new[] { new CultureInfo("en"), new CultureInfo("pt-BR") };
    options.DefaultRequestCulture = new RequestCulture("en");
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
    var selectedCulture = culture.StartsWith("pt", StringComparison.OrdinalIgnoreCase) ? "pt-BR" : "en";
    context.Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(selectedCulture)),
        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), IsEssential = true, SameSite = SameSiteMode.Lax, Secure = context.Request.IsHttps });
    var safeReturnUrl = !string.IsNullOrWhiteSpace(returnUrl) && Uri.IsWellFormedUriString(returnUrl, UriKind.Relative) && !returnUrl.StartsWith("//", StringComparison.Ordinal)
        ? returnUrl : "/";
    return Results.LocalRedirect(safeReturnUrl);
}).WithName("SelectCulture");
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();
