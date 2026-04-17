using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using OpenIddict.Abstractions;
using OpenIddict.Server;
using OpenIddict.Server.AspNetCore;
using Volo.Abp.Identity;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace JobInsight.Auth;

public class GoogleGrantHandler : IOpenIddictServerHandler<OpenIddictServerEvents.HandleTokenRequestContext>
{
    public static OpenIddictServerHandlerDescriptor Descriptor { get; }
        = OpenIddictServerHandlerDescriptor.CreateBuilder<OpenIddictServerEvents.HandleTokenRequestContext>()
            .UseScopedHandler<GoogleGrantHandler>()
            .SetOrder(500_000)
            .Build();

    private readonly IdentityUserManager _userManager;
    private readonly IConfiguration _configuration;

    public GoogleGrantHandler(IdentityUserManager userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async ValueTask HandleAsync(OpenIddictServerEvents.HandleTokenRequestContext context)
    {
        if (!string.Equals(context.Request.GrantType, "google_id_token", StringComparison.OrdinalIgnoreCase))
            return;

        var idToken = (string?)context.Request.GetParameter("id_token");
        if (string.IsNullOrEmpty(idToken))
        {
            context.Reject(error: Errors.InvalidGrant, description: "The id_token parameter is missing.");
            return;
        }

        var googleClientId = _configuration["Authentication:Google:ClientId"];

        GoogleJsonWebSignature.Payload payload;
        try
        {
            payload = await GoogleJsonWebSignature.ValidateAsync(idToken, new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { googleClientId }
            });
        }
        catch
        {
            context.Reject(error: Errors.InvalidGrant, description: "The Google ID token is invalid or expired.");
            return;
        }

        var user = await _userManager.FindByEmailAsync(payload.Email);
        if (user == null)
        {
            user = new Volo.Abp.Identity.IdentityUser(Guid.NewGuid(), payload.Email, payload.Email)
            {
                Name = payload.GivenName ?? string.Empty,
                Surname = payload.FamilyName ?? string.Empty
            };
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                context.Reject(error: Errors.ServerError, description: "Failed to create user account.");
                return;
            }
        }

        var logins = await _userManager.GetLoginsAsync(user);
        if (!logins.Any(l => l.LoginProvider == "Google" && l.ProviderKey == payload.Subject))
        {
            await _userManager.AddLoginAsync(user,
                new Microsoft.AspNetCore.Identity.UserLoginInfo("Google", payload.Subject, "Google"));
        }

        var identity = new ClaimsIdentity(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        identity.AddClaim(Claims.Subject, user.Id.ToString());
        identity.AddClaim(Claims.Email, user.Email!);
        identity.AddClaim(Claims.Name, user.UserName!);
        identity.AddClaim(Claims.PreferredUsername, user.UserName!);

        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
            identity.AddClaim(Claims.Role, role);

        var principal = new ClaimsPrincipal(identity);
        principal.SetScopes(context.Request.GetScopes());
        principal.SetResources(new[] { "JobInsight" });

        context.SignIn(principal);
    }
}
