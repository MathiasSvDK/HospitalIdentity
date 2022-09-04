// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                new IdentityResource(
                name: "custom.name",
                displayName: "Custom Name",
                userClaims: new[] { "name", "firstname", "lastname", "mobilnr","address", "gruppe", "hospitalid", "email" }),
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                   };

		public static IEnumerable<ApiScope> ApiScopes =>
		   new List<ApiScope>
		   {
				new ApiScope
				{
					Name = "api"
				}
		   };
		public static IEnumerable<ApiResource> ApiResources =>
			new[]
			{
				new ApiResource
				{
					Name = "WebApi",
					ApiSecrets = {new Secret("apisecret".Sha256())},

					Scopes = new List<string> { "api" },
				}
			};

		public static IEnumerable<Client> Clients =>
            new Client[]
            {

                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "blazor",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { "https://localhost:5002/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:5002/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
                    AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "custom.name" }
                },
				new Client
				{
					ClientId = "booking",

					ClientSecrets = { new Secret("secret".Sha256()) },

					AllowedGrantTypes = GrantTypes.Code,

					RedirectUris = { "https://localhost:5003/signin-oidc" },
					FrontChannelLogoutUri = "https://localhost:5003/signout-oidc",
					PostLogoutRedirectUris = { "https://localhost:5003/signout-callback-oidc" },
					AlwaysSendClientClaims = true,
					AlwaysIncludeUserClaimsInIdToken = true,
					AllowedScopes = {"openid", "profile", "custom.name", "api" },
					RequireConsent = true,
					RequirePkce = true,
					AllowOfflineAccess = true
					
				},
				new Client
				{
					ClientId = "api",
					ClientSecrets = { new Secret("secret".Sha256()) },

					AllowedGrantTypes = GrantTypes.ClientCredentials,
                    
                    AllowedScopes = { "api" }
				}
			};
    }
}