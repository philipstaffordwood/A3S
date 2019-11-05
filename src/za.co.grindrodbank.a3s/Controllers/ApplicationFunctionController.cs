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
using za.co.grindrodbank.a3s.AbstractApiControllers;
using za.co.grindrodbank.a3s.Helpers;
using System.Security.Claims;

namespace za.co.grindrodbank.a3s.Controllers
{
    public class ApplicationFunctionController : ApplicationFunctionApiController
    {
        private readonly IApplicationFunctionService applicationFunctionService;

        public ApplicationFunctionController(IApplicationFunctionService applicationFunctionService)
        {
            this.applicationFunctionService = applicationFunctionService;
        }

        [Authorize(Policy = "permission:a3s.applicationFunctions.read")]
        public async override Task<IActionResult> ListApplicationFunctionsAsync([FromQuery] bool permissions, [FromQuery] int page, [FromQuery, Range(1, 20)] int size, [FromQuery, StringLength(255, MinimumLength = 0)] string filterDescription, [FromQuery] List<string> orderBy)
        {
            return Ok(await applicationFunctionService.GetListAsync());
        }
    }
}
