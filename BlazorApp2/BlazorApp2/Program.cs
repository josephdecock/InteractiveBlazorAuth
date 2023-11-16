using BlazorApp.Client;
using BlazorApp.Client.Pages;
using BlazorApp.Components;
using Duende.Bff;
using Duende.Bff.Yarp;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();


builder.Services.AddScoped<IRenderModeExplainer, ServerExplainer>();
builder.Services.AddScoped<ICallApi, CallApiFromServer>();

builder.Services.AddBff()
    .AddRemoteApis();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

builder.Services.AddAuthentication(opt =>
    {
        opt.DefaultScheme = "cookie";
        opt.DefaultChallengeScheme = "oidc";
    })
    .AddCookie("cookie", opt =>
    {
        opt.Cookie.Name = "__Host-auto-blazor";
    })
    .AddOpenIdConnect("oidc", opt =>
    {
        opt.Authority = "https://demo.duendesoftware.com";

        opt.ClientId = "interactive.confidential";
        opt.ClientSecret = "secret";
        opt.ResponseType = "code";
        opt.ResponseMode = "query";

        opt.Scope.Clear();
        opt.Scope.Add("openid");
        opt.Scope.Add("profile");
        opt.Scope.Add("api");
        opt.Scope.Add("offline_access");

        opt.MapInboundClaims = false;
        opt.TokenValidationParameters.NameClaimType = "name";
        opt.TokenValidationParameters.RoleClaimType = "role";

        opt.GetClaimsFromUserInfoEndpoint = true;
        opt.SaveTokens = true;
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseBff();
app.UseAuthorization();

app.MapBffManagementEndpoints();

app.MapRemoteBffApiEndpoint("/api/weatherforecast", "https://localhost:7001/weatherforecast")
    .RequireAccessToken(TokenType.User);

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

app.Run();
