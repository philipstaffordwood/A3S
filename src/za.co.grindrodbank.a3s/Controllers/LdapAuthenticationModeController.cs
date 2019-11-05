/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using za.co.grindrodbank.a3s.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using za.co.grindrodbank.a3s.A3SApiResources;
using za.co.grindrodbank.a3s.AbstractApiControllers;
using System.Security.Claims;
using za.co.grindrodbank.a3s.Helpers;

namespace za.co.grindrodbank.a3s.Controllers
{
    public class LdapAuthenticationModeController : LdapAuthenticationModeApiController
    {
        private readonly ILdapAuthenticationModeService authenticationModeService;

        public LdapAuthenticationModeController(ILdapAuthenticationModeService authenticationModeService)
        {
            this.authenticationModeService = authenticationModeService;
        }

        [Authorize(Policy = "permission:a3s.ldapAuthenticationModes.create")]
        public override async Task<IActionResult> CreateLdapAuthenticationModeAsync([FromBody] LdapAuthenticationModeSubmit ldapAuthenticationModeSubmit)
        {
            var loggedOnUser = ClaimsHelper.GetScalarClaimValue<Guid>(User, ClaimTypes.NameIdentifier, Guid.Empty);
            return Ok(await authenticationModeService.CreateAsync(ldapAuthenticationModeSubmit, loggedOnUser));
        }

        [Authorize(Policy = "permission:a3s.ldapAuthenticationModes.read")]
        public override async Task<IActionResult> GetLdapAuthenticationModeAsync([FromRoute, Required] Guid ldapAuthenticationModeId)
        {
            var authenticationMode = await authenticationModeService.GetByIdAsync(ldapAuthenticationModeId);

            if (authenticationMode == null)
                return NotFound();

            return Ok(authenticationMode);
        }

        [Authorize(Policy = "permission:a3s.ldapAuthenticationModes.read")]
        public override async Task<IActionResult> ListLdapAuthenticationModesAsync([FromQuery] List<string> orderBy)
        {
            return Ok(await authenticationModeService.GetListAsync());
        }

        public override async Task<IActionResult> TestLdapAuthenticationModeAsync([FromBody] LdapAuthenticationModeSubmit ldapAuthenticationModeSubmit)
        {
            return Ok(await authenticationModeService.TestAsync(ldapAuthenticationModeSubmit));
        }

        [Authorize(Policy = "permission:a3s.ldapAuthenticationModes.update")]
        public override async Task<IActionResult> UpdateLdapAuthenticationModeAsync([FromRoute, Required] Guid ldapAuthenticationModeId, [FromBody] LdapAuthenticationModeSubmit ldapAuthenticationModeSubmit)
        {
            if (ldapAuthenticationModeId == Guid.Empty || ldapAuthenticationModeSubmit.Uuid == Guid.Empty)
                return BadRequest();

            var loggedOnUser = ClaimsHelper.GetScalarClaimValue<Guid>(User, ClaimTypes.NameIdentifier, Guid.Empty);
            return Ok(await authenticationModeService.UpdateAsync(ldapAuthenticationModeSubmit, loggedOnUser));
        }

        public async override Task<IActionResult> DeleteLdapAuthenticationModeAsync([FromRoute, Required] Guid ldapAuthenticationModeId)
        {
            await authenticationModeService.DeleteAsync(ldapAuthenticationModeId);
            return NoContent();
        }
    }
}
