﻿using Duende.IdentityServer.Models;
using IdentityModel;

namespace IdentityProvider;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("photoRest") { UserClaims = new string [] { JwtClaimTypes.Name }},
            new ApiScope("commentGrpc") { UserClaims = new string [] { JwtClaimTypes.Name }},
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "blazorcource.server",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:7106/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:7106/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:7106/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "photoRest", "commentGrpc" }
            },
        };
}
