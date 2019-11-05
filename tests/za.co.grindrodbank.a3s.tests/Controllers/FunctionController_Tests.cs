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
    public class FunctionController_Tests
    {
        [Fact]
        public async Task GetFunctionAsync_WithEmptyGuid_ReturnsBadRequest()
        {
            // Arrange
            var functionService = Substitute.For<IFunctionService>();
            var controller = new FunctionController(functionService);

            // Act
            var result = await controller.GetFunctionAsync(Guid.Empty);

            // Assert
            var badRequestResult = result as BadRequestResult;
            Assert.NotNull(badRequestResult);
        }

        [Fact]
        public async Task GetFunctionAsync_WithRandomGuid_ReturnsNotFoundResult()
        {
            // Arrange
            var functionService = Substitute.For<IFunctionService>();
            var controller = new FunctionController(functionService);

            // Act
            var result = await controller.GetFunctionAsync(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetFunctionAsync_WithTestGuid_ReturnsCorrectResult()
        {
            // Arrange
            var functionService = Substitute.For<IFunctionService>();
            var testGuid = Guid.NewGuid();
            var testName = "TestUserName";

            functionService.GetByIdAsync(testGuid).Returns(new Function { Uuid = testGuid, Name = testName });

            var controller = new FunctionController(functionService);

            // Act
            IActionResult actionResult = await controller.GetFunctionAsync(testGuid);

            // Assert
            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);

            var function = okResult.Value as Function;
            Assert.NotNull(function);
            Assert.True(function.Uuid == testGuid, $"Retrieved Id {function.Uuid} not the same as sample id {testGuid}.");
            Assert.True(function.Name == testName, $"Retrieved Name {function.Name} not the same as sample id {testName}.");
        }

        [Fact]
        public async Task ListFunctionsAsync_WithNoInputs_ReturnsList()
        {
            // Arrange
            var functionService = Substitute.For<IFunctionService>();

            var inList = new List<Function>();
            inList.Add(new Function { Name = "Test Functions 1", Uuid = Guid.NewGuid() });
            inList.Add(new Function { Name = "Test Functions 2", Uuid = Guid.NewGuid() });
            inList.Add(new Function { Name = "Test Functions 3", Uuid = Guid.NewGuid() });

            functionService.GetListAsync().Returns(inList);

            var controller = new FunctionController(functionService);

            // Act
            IActionResult actionResult = await controller.ListFunctionsAsync(false, 0, 50, string.Empty, null);

            // Assert
            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);

            var outList = okResult.Value as List<Function>;
            Assert.NotNull(outList);

            for (var i = 0; i < outList.Count; i++)
            {
                Assert.Equal(outList[i].Uuid, inList[i].Uuid);
                Assert.Equal(outList[i].Name, inList[i].Name);
            }
        }

        [Fact]
        public async Task UpdateFunctionAsync_WithRandomGuid_ReturnsNotImplemented()
        {
            // Arrange
            var functionService = Substitute.For<IFunctionService>();
            var controller = new FunctionController(functionService);

            // Act
            Exception exAssert = null;

            try
            {
                IActionResult actionResult = await controller.UpdateFunctionAsync(new Guid(), null);
            }
            catch (Exception ex)
            {
                exAssert = ex;
            }

            // Assert
            Assert.False(exAssert is NotImplementedException, $"Updating record should NOT return NotImplementedException");
        }
    }
}
