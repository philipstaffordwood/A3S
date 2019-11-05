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
using za.co.grindrodbank.a3s.Helpers;
using System.Security.Claims;

namespace za.co.grindrodbank.a3s.Controllers
{
    public class RoleController : RoleApiController
    {
        private readonly IRoleService roleService;

        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        [Authorize(Policy = "permission:a3s.roles.create")]
        public async override Task<IActionResult> CreateRoleAsync([FromBody] RoleSubmit roleSubmit)
        {
            var loggedOnUser = ClaimsHelper.GetScalarClaimValue<Guid>(User, ClaimTypes.NameIdentifier, Guid.Empty);
            return Ok(await roleService.CreateAsync(roleSubmit, loggedOnUser));
        }

        [Authorize(Policy = "permission:a3s.roles.read")]
        public async override Task<IActionResult> GetRoleAsync([FromRoute, Required] Guid roleId)
        {
            if (roleId == Guid.Empty)
                return BadRequest();

            var role = await roleService.GetByIdAsync(roleId);

            if(role == null)
                return NotFound();

            return Ok(role);
        }

        [Authorize(Policy = "permission:a3s.roles.read")]
        public async override Task<IActionResult> ListRolesAsync([FromQuery] bool users, [FromQuery] int page, [FromQuery, Range(1, 20)] int size, [FromQuery, StringLength(255, MinimumLength = 0)] string filterDescription, [FromQuery] List<string> orderBy)
        {
            return Ok(await roleService.GetListAsync());
        }

        [Authorize(Policy = "permission:a3s.roles.update")]
        public async override Task<IActionResult> UpdateRoleAsync([FromRoute, Required] Guid roleId, [FromBody] RoleSubmit roleSubmit)
        {
            if (roleId == Guid.Empty || roleSubmit.Uuid == Guid.Empty)
                return BadRequest();

            var loggedOnUser = ClaimsHelper.GetScalarClaimValue<Guid>(User, ClaimTypes.NameIdentifier, Guid.Empty);
            return Ok(await roleService.UpdateAsync(roleSubmit, loggedOnUser));
        }
    }
}
