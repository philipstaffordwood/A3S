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
    public class TeamController : TeamApiController
    {
        private readonly ITeamService teamService;

        public TeamController(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        [Authorize(Policy = "permission:a3s.teams.create")]
        public async override Task<IActionResult> CreateTeamAsync([FromBody] TeamSubmit teamSubmit)
        {
            var loggedOnUser = ClaimsHelper.GetScalarClaimValue<Guid>(User, ClaimTypes.NameIdentifier, Guid.Empty);
            return Ok(await teamService.CreateAsync(teamSubmit, loggedOnUser));
        }

        [Authorize(Policy = "permission:a3s.teams.read")]
        public async override Task<IActionResult> GetTeamAsync([FromRoute, Required] Guid teamId)
        {
            if (teamId == Guid.Empty)
                return BadRequest();

            var team = await teamService.GetByIdAsync(teamId, true);

            if(team == null)
                return NotFound();

            return Ok(team);
        }

        [Authorize(Policy = "permission:a3s.teams.read")]
        public async override Task<IActionResult> ListTeamsAsync([FromQuery] bool users, [FromQuery] bool teams, [FromQuery] bool policies, [FromQuery] int page, [FromQuery, Range(1, 20)] int size, [FromQuery, StringLength(255, MinimumLength = 0)] string filterDesciption, [FromQuery] List<string> orderBy)
        {
            if (ClaimsHelper.GetDataPolicies(User).Contains("a3s.viewYourTeamsOnly"))
            {
                return Ok(await teamService.GetListAsync(ClaimsHelper.GetScalarClaimValue<Guid>(User, ClaimTypes.NameIdentifier, Guid.Empty)));
            }

            return Ok(await teamService.GetListAsync());
        }

        [Authorize(Policy = "permission:a3s.teams.update")]
        public async override Task<IActionResult> UpdateTeamAsync([FromRoute, Required] Guid teamId, [FromBody] TeamSubmit teamSubmit)
        {
            if (teamId == Guid.Empty || teamSubmit.Uuid == Guid.Empty)
                return BadRequest();

            var loggedOnUser = ClaimsHelper.GetScalarClaimValue<Guid>(User, ClaimTypes.NameIdentifier, Guid.Empty);
            return Ok(await teamService.UpdateAsync(teamSubmit, loggedOnUser));
        }
    }
}
