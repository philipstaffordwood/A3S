/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using za.co.grindrodbank.a3s.AbstractApiControllers;
using za.co.grindrodbank.a3s.Services;

namespace za.co.grindrodbank.a3s.Controllers
{
    public class TwoFactorAuthController : TwoFactorAuthApiController
    {
        ITwoFactorAuthService twoFactorAuthService;
        
        public TwoFactorAuthController(ITwoFactorAuthService twoFactorAuthService)
        {
            this.twoFactorAuthService = twoFactorAuthService;
        }

        [Authorize(Policy = "permission:a3s.twoFactorAuth.remove")]
        public async override Task<IActionResult> RemoveTwoFactorAuthenticationAsync([FromRoute, Required] Guid userId)
        {
            if (userId == Guid.Empty)
                return BadRequest();

            await twoFactorAuthService.RemoveTwoFactorAuthenticationAsync(userId);
            return NoContent();
        }
    }
}
