/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.Threading.Tasks;
using za.co.grindrodbank.a3s.Controllers;
using za.co.grindrodbank.a3s.Services;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;
using za.co.grindrodbank.a3s.A3SApiResources;

namespace za.co.grindrodbank.a3s.tests.Controllers
{
    public class SecurityContractController_Tests
    {
        public SecurityContractController_Tests()
        {
        }

        [Fact]
        public async Task ApplySecurityContractAsync_WithNullInput_ReturnsBadRequest()
        {
            // Arrange
            var securityContractService = Substitute.For<ISecurityContractService>();
            var controller = new SecurityContractController(securityContractService);

            // Act
            var actionResult = await controller.ApplySecurityContractAsync(null);

            // Assert
            var badRequestResult = actionResult as BadRequestResult;
            Assert.NotNull(badRequestResult);
        }

        [Fact]
        public async Task ApplySecurityContractAsync_WithValidNoSectionsInput_ReturnsNoContent()
        {
            // Arrange
            var securityContractService = Substitute.For<ISecurityContractService>();
            var controller = new SecurityContractController(securityContractService);

            // A security contract with all it's sub components set to null is a valid security contract.
            var securityContract = new SecurityContract();

            // Act
            var actionResult = await controller.ApplySecurityContractAsync(securityContract);

            // Assert
            var NoContent = actionResult as NoContentResult;
            Assert.NotNull(NoContent);
        }
    }
}
