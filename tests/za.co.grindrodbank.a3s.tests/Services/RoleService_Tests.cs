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

namespace za.co.grindrodbank.a3s.tests.Services
{
    public class RoleService_Tests
    {
        IMapper mapper;
        RoleModel mockedRole;
        Guid roleGuid;
        Guid functionGuid;

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

            mockedRole = new RoleModel();

            mockedRole.Name = "Test Role";
            mockedRole.Id = roleGuid;

            mockedRole.RoleFunctions = new List<RoleFunctionModel>
            {
                new RoleFunctionModel
                {
                    Role = mockedRole,
                    Function = new FunctionModel
                    {
                        Id = functionGuid,
                        Name = "Test function model",
                        Description = "Test function description model"
                    }
                }
            };
        }

        [Fact]
        public async Task GetById_GivenGuid_ReturnsRoleResource()
        {
            var roleRepository = Substitute.For<IRoleRepository>();
            var userRepository = Substitute.For<IUserRepository>();
            var functionRepository = Substitute.For<IFunctionRepository>();

            roleRepository.GetByIdAsync(roleGuid).Returns(mockedRole);

            var roleService = new RoleService(roleRepository, userRepository, functionRepository, mapper);
            var roleResource = await roleService.GetByIdAsync(roleGuid);

            Assert.True(roleResource.Name == "Test Role", $"Role resource Name: '{roleResource.Name}' does not match expected value: 'Test Role'");
            Assert.True(roleResource.Uuid == roleGuid, $"Role resource UUID: '{roleResource.Uuid}' does not match expected value: '{roleGuid}'");
        }
    }
}
