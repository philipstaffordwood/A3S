/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using za.co.grindrodbank.a3s.Controllers;
using za.co.grindrodbank.a3s.Services;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;
using za.co.grindrodbank.a3s.A3SApiResources;

namespace za.co.grindrodbank.a3s.tests.Controllers
{
    public class ApplicationController_Tests
    {
        [Fact]
        public async Task ListApplicationsAsync_WithNoInputs_ReturnsList()
        {
            // Arrange
            var applicationService = Substitute.For<IApplicationService>();

            var inList = new List<Application>();
            inList.Add(new Application { Name = "Test Applications 1", Uuid = Guid.NewGuid() });
            inList.Add(new Application { Name = "Test Applications 2", Uuid = Guid.NewGuid() });
            inList.Add(new Application { Name = "Test Applications 3", Uuid = Guid.NewGuid() });

            applicationService.GetListAsync().Returns(inList);

            var controller = new ApplicationController(applicationService);

            // Act
            IActionResult actionResult = await controller.ListApplicationsAsync(false, 0, 50, string.Empty, null);

            // Assert
            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);

            var outList = okResult.Value as List<Application>;
            Assert.NotNull(outList);

            for (var i = 0; i < outList.Count; i++)
            {
                Assert.Equal(outList[i].Uuid, inList[i].Uuid);
                Assert.Equal(outList[i].Name, inList[i].Name);
            }
        }
    }
}
