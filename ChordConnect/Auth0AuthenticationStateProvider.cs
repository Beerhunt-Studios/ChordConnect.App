using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Auth0.OidcClient;

namespace ChordConnect;

public class Auth0AuthenticationStateProvider : AuthenticationStateProvider
{
    private ClaimsPrincipal currentUser = new ClaimsPrincipal(new ClaimsIdentity());
    private readonly Auth0Client auth0Client;

    public string? AccessToken { get; private set; }

    public Auth0AuthenticationStateProvider(Auth0Client client)
    {
        auth0Client = client;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync() => 
        Task.FromResult(new AuthenticationState(currentUser));

    public Task LogInAsync()
    {
        var loginTask = LogInAsyncCore();
        NotifyAuthenticationStateChanged(loginTask);

        return loginTask;

        async Task<AuthenticationState> LogInAsyncCore()
        {
            var user = await LoginWithAuth0Async();
            currentUser = user;

            return new AuthenticationState(currentUser);
        }
    }

    private async Task<ClaimsPrincipal> LoginWithAuth0Async()
    {
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity());
        var loginResult = await auth0Client.LoginAsync(new { audience = "chord-connect" });

        if (!loginResult.IsError)
        {
            authenticatedUser = loginResult.User;
        }
        this.AccessToken = loginResult.AccessToken;
        return authenticatedUser;
    }

    public async void LogOut()
    {
        await auth0Client.LogoutAsync();
        currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(currentUser)));
    }
}
