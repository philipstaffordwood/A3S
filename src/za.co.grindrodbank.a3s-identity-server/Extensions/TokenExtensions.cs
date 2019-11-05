/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using Microsoft.AspNetCore.Identity;

namespace za.co.grindrodbank.a3sidentityserver.Extensions
{
    public static class TokenExtensions
    {
        public static string GetAspNetUserStoreProviderName(this TokenOptions tokenOptions)
        {
            if (tokenOptions != null)
                return "[AspNetUserStore]";
            else
                return string.Empty;
        }

        public static string GetAuthenticatorKeyName(this TokenOptions tokenOptions)
        {
            if (tokenOptions != null)
                return "AuthenticatorKey";
            else
                return string.Empty;
        }

        public static string GetRecoverCodesName(this TokenOptions tokenOptions)
        {
            if (tokenOptions != null)
                return "RecoveryCodes";
            else
                return string.Empty;
        }
    }
}
