using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer.Settings
{
    public static class IdentityServerConfig
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("movieAPI", "Movie API"),
                new ApiScope("movie")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[] { };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new()
                {
                    ClientId = "movieClient",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "movieAPI" }
                },
                new()
                {
                    ClientId = "movies_mvc_client",
                    ClientName = "Movies MVC Web App",
                    AllowedGrantTypes = GrantTypes.Hybrid, // Code
                    RequirePkce = false,
                    AllowRememberConsent = false,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:5003/signin-oidc" // Client app port
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:5003/signout-callback-oidc"
                    },
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = new List<string>()
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        "roles",
                        "movieAPI"
                    }
                }
            };

        public static IEnumerable<TestUser> TestUsers =>
            new TestUser[]
            {
                new ()
                {
                    SubjectId = "58E86359-073C-434B-AD2D-A3932222DABE",
                    Username = "Ivan",
                    Password = "Ivan",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName, "Ivan"),
                        new Claim(JwtClaimTypes.FamilyName, "Jevtic")
                    }
                }
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResources.Email(),
                new IdentityResource(
                    "roles",
                    "Your roles",
                    new List<string> { "role" })
            };
    }
}
