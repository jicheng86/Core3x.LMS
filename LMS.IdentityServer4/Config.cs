// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using static IdentityServer4.IdentityServerConstants;

namespace LMS.IdentityServer4
{
    public static class Config
    {
        static readonly string[] allowedScopes =
     {
            StandardScopes.OpenId,
            StandardScopes.Profile,
            StandardScopes.Email,
            "scope1",
            "scope2",
            "transaction"
        };


        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("api1", "My API #1"),
                new ApiResource("api2", "My API #2")
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                ///////////////////////////////////////////
                // JS OIDC Sample
                //////////////////////////////////////////
                new Client
                {
                    ClientId = "js_oidc",
                    ClientName = "JavaScript OIDC Client",
                    ClientUri = "http://identityserver.io",

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,

                    RedirectUris =
                    {
                        "https://localhost:44300/index.html",
                        "https://localhost:44300/callback.html",
                        "https://localhost:44300/silent.html",
                        "https://localhost:44300/popup.html"
                    },

                    PostLogoutRedirectUris = { "https://localhost:44300/index.html" },
                    AllowedCorsOrigins = { "https://localhost:44300" },

                    AllowedScopes = allowedScopes
                },
                
                ///////////////////////////////////////////
                // MVC Automatic Token Management Sample
                //////////////////////////////////////////
                new Client
                {
                    ClientId = "mvc.tokenmanagement",

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,

                    AccessTokenLifetime = 75,

                    RedirectUris = { "https://localhost:44301/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44301/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44301/signout-callback-oidc" },

                    AllowOfflineAccess = true,

                    AllowedScopes = allowedScopes
                },
                
                ///////////////////////////////////////////
                // MVC Code Flow Sample
                //////////////////////////////////////////
                new Client
                {
                    ClientId = "mvc.code",
                    ClientName = "MVC Code Flow",
                    ClientUri = "http://identityserver.io",

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RequireConsent = true,
                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { "https://localhost:44302/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44302/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44302/signout-callback-oidc" },

                    AllowOfflineAccess = true,

                    AllowedScopes = allowedScopes
                },
                
                ///////////////////////////////////////////
                // MVC Hybrid Flow Sample (Back Channel logout)
                //////////////////////////////////////////
                new Client
                {
                    ClientId = "mvc.hybrid.backchannel",
                    ClientName = "MVC Hybrid (with BackChannel logout)",
                    ClientUri = "http://identityserver.io",

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RequirePkce = false,

                    RedirectUris = { "https://localhost:44303/signin-oidc" },
                    BackChannelLogoutUri = "https://localhost:44303/logout",
                    PostLogoutRedirectUris = { "https://localhost:44303/signout-callback-oidc" },

                    AllowOfflineAccess = true,

                    AllowedScopes = allowedScopes
                },
                ///////////////////////////////////////////
                // Console Resource Owner Flow Sample
                //////////////////////////////////////////
                new Client
                {
                    ClientId = "roclient",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        "custom.profile",
                        "scope1", "scope2"
                    }
                },

                ///////////////////////////////////////////
                // Console Public Resource Owner Flow Sample
                //////////////////////////////////////////
                new Client
                {
                    ClientId = "roclient.public",
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        "scope1", "scope2"
                    }
                },

                ///////////////////////////////////////////
                // Console with PKCE Sample
                //////////////////////////////////////////
                new Client
                {
                    ClientId = "console.pkce",
                    ClientName = "Console with PKCE Sample",
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RedirectUris = {"http://127.0.0.1"},
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "scope1", "scope2"
                    }
                },
                ///////////////////////////////////////////
                // WinConsole with PKCE Sample
                //////////////////////////////////////////
                new Client
                {
                    ClientId = "winconsole",
                    ClientName = "Windows Console with PKCE Sample",
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RedirectUris = {"sample-windows-client://callback"},
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    AllowedIdentityTokenSigningAlgorithms = {"ES256"},
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "scope1", "scope2"
                    }
                },
            };

        // identity resources represent identity data about a user that can be requested via the scope parameter (OpenID Connect)
        public static readonly IEnumerable<IdentityResource> IdentityResources =
            new[]
            {
                // some standard scopes from the OIDC spec
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),

                // custom identity resource with some consolidated claims
                new IdentityResource("custom.profile", new[] { JwtClaimTypes.Name, JwtClaimTypes.Email, "location" })
            };

        // API scopes represent values that describe scope of access and can be requested by the scope parameter (OAuth)
        public static readonly IEnumerable<ApiScope> ApiScopes =
            new[]
            {
                // local feature
                new ApiScope(LocalApi.ScopeName),

                // some generic scopes
                new ApiScope("scope1"),
                new ApiScope("scope2"),
                new ApiScope("scope3"),
                new ApiScope("shared.scope"),

                // used as a dynamic scope
                new ApiScope("transaction", "Transaction")
                {
                    Description = "Some Transaction"
                }
            };

        // API resources are more formal representation of a resource with processing rules and their scopes (if any)
        public static readonly IEnumerable<ApiResource> ApiResources =
            new[]
            {
                // simple version with ctor
                new ApiResource("resource1", "Resource 1")
                {
                    // this is needed for introspection when using reference tokens
                    ApiSecrets = { new Secret("secret".Sha256()) },

                    //AllowedSigningAlgorithms = { "RS256", "ES256" }

                    Scopes = { "scope1", "shared.scope" }
                },
                
                // expanded version if more control is needed
                new ApiResource("resource2", "Resource 2")
                {
                    ApiSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    //AllowedSigningAlgorithms = { "PS256", "ES256", "RS256" },

                    UserClaims =
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Email
                    },

                    Scopes = { "scope2", "shared.scope" }
                }
            };
    }
}
