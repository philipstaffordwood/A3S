/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using za.co.grindrodbank.a3s.MappingProfiles;
using za.co.grindrodbank.a3s.Models;
using za.co.grindrodbank.a3s.Repositories;
using za.co.grindrodbank.a3s.Services;
using AutoMapper;
using NSubstitute;
using Xunit;
using za.co.grindrodbank.a3s.A3SApiResources;
using za.co.grindrodbank.a3s.Exceptions;

namespace za.co.grindrodbank.a3s.tests.Services
{
    public class RoleService_Tests
    {
        private readonly IMapper mapper;
        private readonly RoleModel mockedRoleModel;
        private readonly RoleSubmit mockedRoleSubmitModel;
        private readonly Guid roleGuid;
        private readonly Guid functionGuid;

        public RoleService_Tests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RoleSubmitResourceRoleModelProfile());
                cfg.AddProfile(new RoleResourceRoleModelProfile());
            });

            mapper = config.CreateMapper();
            roleGuid = Guid.NewGuid();
            functionGuid = Guid.NewGuid();

            mockedRoleModel = new RoleModel
            {
                Name = "Test Role",
                Id = roleGuid
            };

            mockedRoleModel.RoleFunctions = new List<RoleFunctionModel>
            {
                new RoleFunctionModel
                {
                    Role = mockedRoleModel,
                    Function = new FunctionModel
                    {
                        Id = functionGuid,
                        Name = "Test function model",
                        Description = "Test function description model"
                    }
                }
            };

            mockedRoleSubmitModel = new RoleSubmit()
            {
                Uuid = mockedRoleModel.Id,
                Name = mockedRoleModel.Name,
                FunctionIds = new List<Guid>()
            };

            foreach (var function in mockedRoleModel.RoleFunctions)
                mockedRoleSubmitModel.FunctionIds.Add(function.FunctionId);
        }

        [Fact]
        public async Task GetById_GivenGuid_ReturnsRoleResource()
        {
            var roleRepository = Substitute.For<IRoleRepository>();
            var userRepository = Substitute.For<IUserRepository>();
            var functionRepository = Substitute.For<IFunctionRepository>();

            roleRepository.GetByIdAsync(roleGuid).Returns(mockedRoleModel);

            var roleService = new RoleService(roleRepository, userRepository, functionRepository, mapper);
            var roleResource = await roleService.GetByIdAsync(roleGuid);

            Assert.True(roleResource.Name == "Test Role", $"Role resource Name: '{roleResource.Name}' does not match expected value: 'Test Role'");
            Assert.True(roleResource.Uuid == roleGuid, $"Role resource UUID: '{roleResource.Uuid}' does not match expected value: '{roleGuid}'");
        }

        [Fact]
        public async Task CreateAsync_GivenFullProcessableModel_ReturnsCreatedModel()
        {
            // Arrange
            var roleRepository = Substitute.For<IRoleRepository>();
            var userRepository = Substitute.For<IUserRepository>();
            var functionRepository = Substitute.For<IFunctionRepository>();

            functionRepository.GetByIdAsync(mockedRoleModel.RoleFunctions[0].FunctionId)
                .Returns(mockedRoleModel.RoleFunctions[0].Function);
            roleRepository.CreateAsync(Arg.Any<RoleModel>()).Returns(mockedRoleModel);

            var roleService = new RoleService(roleRepository, userRepository, functionRepository, mapper);

            // Act
            var roleResource = await roleService.CreateAsync(mockedRoleSubmitModel, Guid.NewGuid());

            // Assert
            Assert.True(roleResource.Name == mockedRoleSubmitModel.Name, $"Role Resource name: '{roleResource.Name}' not the expected value: '{mockedRoleSubmitModel.Name}'");
            Assert.True(roleResource.FunctionIds.Count == mockedRoleSubmitModel.FunctionIds.Count, $"Role Resource Functions Count: '{roleResource.FunctionIds.Count}' not the expected value: '{mockedRoleSubmitModel.FunctionIds.Count}'");
        }

        [Fact]
        public async Task CreateAsync_GivenAlreadyUsedName_ThrowsItemNotProcessableException()
        {
            var roleRepository = Substitute.For<IRoleRepository>();
            var userRepository = Substitute.For<IUserRepository>();
            var functionRepository = Substitute.For<IFunctionRepository>();

            functionRepository.GetByIdAsync(mockedRoleModel.RoleFunctions[0].FunctionId)
                .Returns(mockedRoleModel.RoleFunctions[0].Function);
            roleRepository.GetByIdAsync(mockedRoleModel.Id).Returns(mockedRoleModel);
            roleRepository.GetByNameAsync(mockedRoleSubmitModel.Name).Returns(mockedRoleModel);
            roleRepository.CreateAsync(Arg.Any<RoleModel>()).Returns(mockedRoleModel);

            var roleService = new RoleService(roleRepository, userRepository, functionRepository, mapper);

            // Act
            Exception caughEx = null;
            try
            {
                var roleResource = await roleService.CreateAsync(mockedRoleSubmitModel, Guid.NewGuid());
            }
            catch (Exception ex)
            {
                caughEx = ex;
            }

            // Assert
            Assert.True(caughEx is ItemNotProcessableException, "Attempted create with an already used name must throw an ItemNotProcessableException.");
        }

        [Fact]
        public async Task GetListAsync_Executed_ReturnsList()
        {
            // Arrange
            var roleRepository = Substitute.For<IRoleRepository>();
            var userRepository = Substitute.For<IUserRepository>();
            var functionRepository = Substitute.For<IFunctionRepository>();

            roleRepository.GetListAsync().Returns(
                new List<RoleModel>()
                {
                    mockedRoleModel,
                    mockedRoleModel
                });

            var roleService = new RoleService(roleRepository, userRepository, functionRepository, mapper);

            // Act
            var roleList = await roleService.GetListAsync();

            // Assert
            Assert.True(roleList.Count == 2, "Expected list count is 2");
            Assert.True(roleList[0].Name == mockedRoleModel.Name, $"Expected applicationRole name: '{roleList[0].Name}' does not equal expected value: '{mockedRoleModel.Name}'");
            Assert.True(roleList[0].Uuid == mockedRoleModel.Id, $"Expected applicationRole UUID: '{roleList[0].Uuid}' does not equal expected value: '{mockedRoleModel.Id}'");
        }

        [Fact]
        public async Task UpdateAsync_GivenFullProcessableModel_ReturnsUpdatedModel()
        {
            // Arrange
            var roleRepository = Substitute.For<IRoleRepository>();
            var userRepository = Substitute.For<IUserRepository>();
            var functionRepository = Substitute.For<IFunctionRepository>();

            functionRepository.GetByIdAsync(mockedRoleModel.RoleFunctions[0].FunctionId)
                .Returns(mockedRoleModel.RoleFunctions[0].Function);
            roleRepository.GetByIdAsync(mockedRoleModel.Id).Returns(mockedRoleModel);
            roleRepository.UpdateAsync(Arg.Any<RoleModel>()).Returns(mockedRoleModel);

            var roleService = new RoleService(roleRepository, userRepository, functionRepository, mapper);

            // Act
            var roleResource = await roleService.UpdateAsync(mockedRoleSubmitModel, Guid.NewGuid());

            // Assert
            Assert.True(roleResource.Name == mockedRoleSubmitModel.Name, $"Role Resource name: '{roleResource.Name}' not the expected value: '{mockedRoleSubmitModel.Name}'");
            Assert.True(roleResource.FunctionIds.Count == mockedRoleSubmitModel.FunctionIds.Count, $"Role Resource Permission Count: '{roleResource.FunctionIds.Count}' not the expected value: '{mockedRoleSubmitModel.FunctionIds.Count}'");
        }

        [Fact]
        public async Task UpdateAsync_GivenUnfindableRole_ThrowsItemNotFoundException()
        {
            // Arrange
            var roleRepository = Substitute.For<IRoleRepository>();
            var userRepository = Substitute.For<IUserRepository>();
            var functionRepository = Substitute.For<IFunctionRepository>();

            functionRepository.GetByIdAsync(mockedRoleModel.RoleFunctions[0].FunctionId)
                .Returns(mockedRoleModel.RoleFunctions[0].Function);
            roleRepository.UpdateAsync(Arg.Any<RoleModel>()).Returns(mockedRoleModel);

            var roleService = new RoleService(roleRepository, userRepository, functionRepository, mapper);

            // Act
            Exception caughEx = null;
            try
            {
                var roleResource = await roleService.UpdateAsync(mockedRoleSubmitModel, Guid.NewGuid());
            }
            catch (Exception ex)
            {
                caughEx = ex;
            }

            // Assert
            Assert.True(caughEx is ItemNotFoundException, "Unfindable roles must throw an ItemNotFoundException");
        }

        [Fact]
        public async Task UpdateAsync_GivenNewTakenName_ThrowsItemNotProcessableException()
        {
            // Arrange
            var roleRepository = Substitute.For<IRoleRepository>();
            var userRepository = Substitute.For<IUserRepository>();
            var functionRepository = Substitute.For<IFunctionRepository>();

            mockedRoleSubmitModel.Name += "_changed_name";

            functionRepository.GetByIdAsync(mockedRoleModel.RoleFunctions[0].FunctionId)
                .Returns(mockedRoleModel.RoleFunctions[0].Function);
            roleRepository.GetByIdAsync(mockedRoleModel.Id).Returns(mockedRoleModel);
            roleRepository.GetByNameAsync(mockedRoleSubmitModel.Name).Returns(mockedRoleModel);
            roleRepository.UpdateAsync(Arg.Any<RoleModel>()).Returns(mockedRoleModel);

            var roleService = new RoleService(roleRepository, userRepository, functionRepository, mapper);

            // Act
            Exception caughEx = null;
            try
            {
                var roleResource = await roleService.UpdateAsync(mockedRoleSubmitModel, Guid.NewGuid());
            }
            catch (Exception ex)
            {
                caughEx = ex;
            }

            // Assert
            Assert.True(caughEx is ItemNotProcessableException, "New taken name must throw an ItemNotProcessableException");
        }

        [Fact]
        public async Task UpdateAsync_GivenNewTUntakenName_ReturnsUpdatedRole()
        {
            // Arrange
            var roleRepository = Substitute.For<IRoleRepository>();
            var userRepository = Substitute.For<IUserRepository>();
            var functionRepository = Substitute.For<IFunctionRepository>();

            mockedRoleSubmitModel.Name += "_changed_name";

            functionRepository.GetByIdAsync(mockedRoleModel.RoleFunctions[0].FunctionId)
                .Returns(mockedRoleModel.RoleFunctions[0].Function);
            roleRepository.GetByIdAsync(mockedRoleModel.Id).Returns(mockedRoleModel);
            roleRepository.UpdateAsync(Arg.Any<RoleModel>()).Returns(mockedRoleModel);

            var roleService = new RoleService(roleRepository, userRepository, functionRepository, mapper);

            // Act
            var roleResource = await roleService.UpdateAsync(mockedRoleSubmitModel, Guid.NewGuid());

            // Assert
            Assert.True(roleResource.Name == mockedRoleSubmitModel.Name, $"Role Resource name: '{roleResource.Name}' not the expected value: '{mockedRoleSubmitModel.Name}'");
            Assert.True(roleResource.FunctionIds.Count == mockedRoleSubmitModel.FunctionIds.Count, $"Role Resource Permission Count: '{roleResource.FunctionIds.Count}' not the expected value: '{mockedRoleSubmitModel.FunctionIds.Count}'");
        }
    }
}
