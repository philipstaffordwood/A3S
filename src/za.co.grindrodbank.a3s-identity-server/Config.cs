/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace za.co.grindrodbank.a3sidentityserver
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("a3s", "A3S management API.", new[] { "permission"}),
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // This is the default A3S client, intended to be used for bootstrapping the configuration.
                new Client
                {
                    ClientId = "a3s-default",
                    ClientName = "Default A3S client.",
                    AllowedGrantTypes = new List<string> {"password" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "a3s"
                    },

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowOfflineAccess = true,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequireConsent = false
                }
            };
        }
    }
}