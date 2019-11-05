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
    public class ApplicationFunctionController_Tests
    {
        [Fact]
        public async Task ListApplicationFunctionsAsync_WithNoInputs_ReturnsList()
        {
            // Arrange
            var applicationFunctionService = Substitute.For<IApplicationFunctionService>();

            var inList = new List<ApplicationFunction>();
            inList.Add(new ApplicationFunction { Name = "Test ApplicationFunctions 1", Uuid = Guid.NewGuid() });
            inList.Add(new ApplicationFunction { Name = "Test ApplicationFunctions 2", Uuid = Guid.NewGuid() });
            inList.Add(new ApplicationFunction { Name = "Test ApplicationFunctions 3", Uuid = Guid.NewGuid() });

            applicationFunctionService.GetListAsync().Returns(inList);

            var controller = new ApplicationFunctionController(applicationFunctionService);

            // Act
            IActionResult actionResult = await controller.ListApplicationFunctionsAsync(false, 0, 50, string.Empty, null);

            // Assert
            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);

            var outList = okResult.Value as List<ApplicationFunction>;
            Assert.NotNull(outList);

            for (var i = 0; i < outList.Count; i++)
            {
                Assert.Equal(outList[i].Uuid, inList[i].Uuid);
                Assert.Equal(outList[i].Name, inList[i].Name);
            }
        }
    }
}
