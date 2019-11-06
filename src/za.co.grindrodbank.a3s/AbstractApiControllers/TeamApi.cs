/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
/*
 * A3S
 *
 * API Definition for the A3S. This service allows authentication, authorisation and accounting.
 *
 * The version of the OpenAPI document: 1.0.0
 * 
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using za.co.grindrodbank.a3s.Attributes;
using Microsoft.AspNetCore.Authorization;
using za.co.grindrodbank.a3s.A3SApiResources;

namespace za.co.grindrodbank.a3s.AbstractApiControllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public abstract class TeamApiController : ControllerBase
    { 
        /// <summary>
        /// Creates a new team.
        /// </summary>
        /// <remarks>Create a new team.</remarks>
        /// <param name="teamSubmit"></param>
        /// <response code="200">OK.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="401">Not authenticated.</response>
        /// <response code="403">Forbidden - You are not authorized to create the team.</response>
        /// <response code="404">Team related entity not found.</response>
        /// <response code="422">Non-Processible Entity. The request was correctly structured, but some business rules were violated, preventing the creation of the team.</response>
        /// <response code="500">An unexpected error occurred</response>
        [HttpPost]
        [Route("/teams")]
        [ValidateModelState]
        [ProducesResponseType(statusCode: 200, type: typeof(Team))]
        [ProducesResponseType(statusCode: 400, type: typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 401, type: typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 403, type: typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 404, type: typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 422, type: typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 500, type: typeof(ErrorResponse))]
        public abstract Task<IActionResult> CreateTeamAsync([FromBody]TeamSubmit teamSubmit);

        /// <summary>
        /// Get a team.
        /// </summary>
        /// <remarks>Get a team by its UUID.</remarks>
        /// <param name="teamId">team</param>
        /// <response code="200">OK.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="401">Not authenticated.</response>
        /// <response code="403">Forbidden - You are not authorized to access the team.</response>
        /// <response code="404">Team not found.</response>
        /// <response code="500">An unexpected error occurred.</response>
        [HttpGet]
        [Route("/teams/{teamId}")]
        [ValidateModelState]
        [ProducesResponseType(statusCode: 200, type: typeof(Team))]
        [ProducesResponseType(statusCode: 400, type: typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 401, type: typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 403, type: typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 404, type: typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 500, type: typeof(ErrorResponse))]
        public abstract Task<IActionResult> GetTeamAsync([FromRoute][Required]Guid teamId);

        /// <summary>
        /// Search for teams.
        /// </summary>
        /// <remarks>Search for teams.</remarks>
        /// <param name="users">Whether to fill in the users member field</param>
        /// <param name="teams">Whether to fill in the teams member field</param>
        /// <param name="policies">Whether to fill in the policies member field</param>
        /// <param name="page">The page to view.</param>
        /// <param name="size">The size of a page.</param>
        /// <param name="filterDesciption">A search query filter on the team&#39;s description</param>
        /// <param name="orderBy">a comma separated list of fields in their sort order. Ascending order is assumed. Append desc after a field to indicate descending order.</param>
        /// <response code="200">OK.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="401">Not authenticated.</response>
        /// <response code="403">Forbidden - You are not authorized to access the list of teams.</response>
        /// <response code="404">Teams list not found.</response>
        /// <response code="500">An unexpected error occurred.</response>
        [HttpGet]
        [Route("/teams")]
        [ValidateModelState]
        [ProducesResponseType(statusCode: 200, type: typeof(List<Team>))]
        [ProducesResponseType(statusCode: 400, type: typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 401, type: typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 403, type: typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 404, type: typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 500, type: typeof(ErrorResponse))]
        public abstract Task<IActionResult> ListTeamsAsync([FromQuery]bool users, [FromQuery]bool teams, [FromQuery]bool policies, [FromQuery]int page, [FromQuery][Range(1, 20)]int size, [FromQuery][StringLength(255, MinimumLength=0)]string filterDesciption, [FromQuery]List<string> orderBy);

        /// <summary>
        /// Update a team.
        /// </summary>
        /// <remarks>Update a team by its UUID.</remarks>
        /// <param name="teamId">The UUID of the team.</param>
        /// <param name="teamSubmit"></param>
        /// <response code="200">OK.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="401">Not authenticated.</response>
        /// <response code="403">Forbidden - You are not authorized to update the team.</response>
        /// <response code="404">Teams not found.</response>
        /// <response code="422">Non-Processible Entity - The requests was correctly structured, but some business rules were violated, preventing the update.</response>
        /// <response code="500">An unexpected error occurred.</response>
        [HttpPut]
        [Route("/teams/{teamId}")]
        [ValidateModelState]
        [ProducesResponseType(statusCode: 200, type: typeof(Team))]
        [ProducesResponseType(statusCode: 400, type: typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 401, type: typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 403, type: typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 404, type: typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 422, type: typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 500, type: typeof(ErrorResponse))]
        public abstract Task<IActionResult> UpdateTeamAsync([FromRoute][Required]Guid teamId, [FromBody]TeamSubmit teamSubmit);
    }
}
