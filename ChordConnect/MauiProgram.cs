using Auth0.OidcClient;
using ChordConnect.Api;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace ChordConnect
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton(new Auth0Client(new()
            {
                Domain = "beerhuntstudios.eu.auth0.com",
                ClientId = "y5jnnIh4NKvUX0etemAjN5B9eLWabh2v",
                RedirectUri = "ch.beerhuntstudios.chordconnect://callback/",
                PostLogoutRedirectUri = "ch.beerhuntstudios.chordconnect://callback/",
                Scope = "openid"
            }));
            builder.Services.AddScoped(x => {
                return new ChordAPI("http://192.168.1.203", (x.GetRequiredService<AuthenticationStateProvider>() as Auth0AuthenticationStateProvider).AccessToken);
            });
            builder.Services
                .AddAuthorizationCore()
                .AddCascadingAuthenticationState();
            builder.Services.AddScoped<AuthenticationStateProvider, Auth0AuthenticationStateProvider>();
            builder.Services.AddMudServices();

            return builder.Build();
        }
    }
}
