/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.Threading.Tasks;

namespace za.co.grindrodbank.a3s.Services
{
    public interface ITwoFactorAuthService
    {
        Task RemoveTwoFactorAuthenticationAsync(Guid userId);
    }
}
