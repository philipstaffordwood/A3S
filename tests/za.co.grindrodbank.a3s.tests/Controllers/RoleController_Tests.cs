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
    public class RoleController_Tests
    {
        [Fact]
        public async Task GetRoleAsync_WithEmptyGuid_ReturnsBadRequest()
        {
            // Arrange
            var roleService = Substitute.For<IRoleService>();
            var controller = new RoleController(roleService);

            // Act
            var result = await controller.GetRoleAsync(Guid.Empty);

            // Assert
            var badRequestResult = result as BadRequestResult;
            Assert.NotNull(badRequestResult);
        }

        [Fact]
        public async Task GetRoleAsync_WithRandomGuid_ReturnsNotFoundResult()
        {
            // Arrange
            var roleService = Substitute.For<IRoleService>();
            var controller = new RoleController(roleService);

            // Act
            var result = await controller.GetRoleAsync(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetRoleAsync_WithTestGuid_ReturnsCorrectResult()
        {
            // Arrange
            var roleService = Substitute.For<IRoleService>();
            var testGuid = Guid.NewGuid();
            var testName = "TestUserName";

            roleService.GetByIdAsync(testGuid).Returns(new Role { Uuid = testGuid, Name = testName });

            var controller = new RoleController(roleService);

            // Act
            IActionResult actionResult = await controller.GetRoleAsync(testGuid);

            // Assert
            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);

            var role = okResult.Value as Role;
            Assert.NotNull(role);
            Assert.True(role.Uuid == testGuid, $"Retrieved Id {role.Uuid} not the same as sample id {testGuid}.");
            Assert.True(role.Name == testName, $"Retrieved Name {role.Name} not the same as sample id {testName}.");
        }

        [Fact]
        public async Task ListRolesAsync_WithNoInputs_ReturnsList()
        {
            // Arrange
            var roleService = Substitute.For<IRoleService>();

            var inList = new List<Role>();
            inList.Add(new Role { Name = "Test Roles 1", Uuid = Guid.NewGuid() });
            inList.Add(new Role { Name = "Test Roles 2", Uuid = Guid.NewGuid() });
            inList.Add(new Role { Name = "Test Roles 3", Uuid = Guid.NewGuid() });

            roleService.GetListAsync().Returns(inList);

            var controller = new RoleController(roleService);

            // Act
            IActionResult actionResult = await controller.ListRolesAsync(false, 0, 50, string.Empty, null);

            // Assert
            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);

            var outList = okResult.Value as List<Role>;
            Assert.NotNull(outList);

            for (var i = 0; i < outList.Count; i++)
            {
                Assert.Equal(outList[i].Uuid, inList[i].Uuid);
                Assert.Equal(outList[i].Name, inList[i].Name);
            }
        }

        [Fact]
        public async Task UpdateRoleAsync_WithEmptyGuid_ReturnsBadRequest()
        {
            // Arrange
            var roleService = Substitute.For<IRoleService>();
            var controller = new RoleController(roleService);

            // Act
            IActionResult actionResult = await controller.UpdateRoleAsync(Guid.Empty, null);

            // Assert
            var badRequestResult = actionResult as BadRequestResult;
            Assert.NotNull(badRequestResult);
        }

        [Fact]
        public async Task UpdateRoleAsync_WithTestRole_ReturnsUpdatedRole()
        {
            // Arrange
            var roleService = Substitute.For<IRoleService>();
            var inputModel = new RoleSubmit()
            {
                Uuid = Guid.NewGuid(),
                Name = "Test Role Name",
                FunctionIds = new List<Guid>()
                {
                    new Guid(),
                    new Guid(),
                }
            };

            roleService.UpdateAsync(inputModel, Arg.Any<Guid>())
                .Returns(new Role()
                {
                    Uuid = inputModel.Uuid,
                    Name = inputModel.Name,
                    FunctionIds = inputModel.FunctionIds
                }
                );

            var controller = new RoleController(roleService);

            // Act
            IActionResult actionResult = await controller.UpdateRoleAsync(inputModel.Uuid, inputModel);

            // Assert
            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);

            var role = okResult.Value as Role;
            Assert.NotNull(role);
            Assert.True(role.Uuid == inputModel.Uuid, $"Retrieved Id {role.Uuid} not the same as sample id {inputModel.Uuid}.");
            Assert.True(role.Name == inputModel.Name, $"Retrieved Name {role.Name} not the same as sample Name {inputModel.Name}.");
            Assert.True(role.FunctionIds[0] == inputModel.FunctionIds[0], $"Retrieved function id {role.FunctionIds[0]} not the same as sample function id {inputModel.FunctionIds[0]}.");
            Assert.True(role.FunctionIds[1] == inputModel.FunctionIds[1], $"Retrieved function id {role.FunctionIds[1]} not the same as sample function id {inputModel.FunctionIds[1]}.");
        }

        [Fact]
        public async Task CreateRoleAsync_WithTestRole_ReturnsCreatesdRole()
        {
            // Arrange
            var roleService = Substitute.For<IRoleService>();
            var inputModel = new RoleSubmit()
            {
                Uuid = Guid.NewGuid(),
                Name = "Test Role Name",
                FunctionIds = new List<Guid>()
                {
                    new Guid(),
                    new Guid(),
                }
            };

            roleService.CreateAsync(inputModel, Arg.Any<Guid>())
                .Returns(new Role()
                {
                    Uuid = inputModel.Uuid,
                    Name = inputModel.Name,
                    FunctionIds = inputModel.FunctionIds
                }
                );

            var controller = new RoleController(roleService);

            // Act
            IActionResult actionResult = await controller.CreateRoleAsync(inputModel);

            // Assert
            var okResult = actionResult as OkObjectResult;
            Assert.NotNull(okResult);

            var role = okResult.Value as Role;
            Assert.NotNull(role);
            Assert.True(role.Uuid == inputModel.Uuid, $"Retrieved Id {role.Uuid} not the same as sample id {inputModel.Uuid}.");
            Assert.True(role.Name == inputModel.Name, $"Retrieved Name {role.Name} not the same as sample Name {inputModel.Name}.");
            Assert.True(role.FunctionIds[0] == inputModel.FunctionIds[0], $"Retrieved function id {role.FunctionIds[0]} not the same as sample function id {inputModel.FunctionIds[0]}.");
            Assert.True(role.FunctionIds[1] == inputModel.FunctionIds[1], $"Retrieved function id {role.FunctionIds[1]} not the same as sample function id {inputModel.FunctionIds[1]}.");
        }
    }
}
