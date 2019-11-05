/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using za.co.grindrodbank.a3s.MappingProfiles;
using za.co.grindrodbank.a3s.Models;
using za.co.grindrodbank.a3s.Repositories;
using za.co.grindrodbank.a3s.Services;
using AutoMapper;
using NSubstitute;
using Xunit;
using za.co.grindrodbank.a3s.A3SApiResources;

namespace za.co.grindrodbank.a3s.tests.Services
{
    public class FunctionService_Tests
    {
        IMapper mapper;
        FunctionModel mockedFunctionModel;
        Guid guid;

        public FunctionService_Tests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new FunctionResourceFunctionModelProfile());
                cfg.AddProfile(new PermissionResourcePermisionModelProfile());
            });

            mapper = config.CreateMapper();

            guid = Guid.NewGuid();
            var applicationGuid = Guid.NewGuid();
            var permissionsGuid = Guid.NewGuid();

            mockedFunctionModel = new FunctionModel();
            mockedFunctionModel.Id = guid;
            mockedFunctionModel.Name = "Test function name";
            mockedFunctionModel.Description = "Test description";
            mockedFunctionModel.Application = new ApplicationModel
            {
                Name = "Test Application",
                Id = applicationGuid
            };

            mockedFunctionModel.FunctionPermissions = new List<FunctionPermissionModel>
            {
                new FunctionPermissionModel
                {
                    Function = mockedFunctionModel,
                    Permission = new PermissionModel
                    {
                        Name = "Test permission",
                        Description = "Test permissions description",
                        Id = permissionsGuid
                    }
                }
            };
        }

        [Fact]
        public async Task GetById_GivenGuid_ReturnsFunctionResource()
        {
            var functionRepository = Substitute.For<IFunctionRepository>();
            var permissionRepository = Substitute.For<IPermissionRepository>();
            var applicationRepository = Substitute.For<IApplicationRepository>();

            functionRepository.GetByIdAsync(guid).Returns(mockedFunctionModel);

            var functionService = new FunctionService(functionRepository, permissionRepository, applicationRepository, mapper);
            var functionResource = await functionService.GetByIdAsync(guid);

            Assert.True(functionResource.Name == "Test function name", $"Function Resource name: '{functionResource.Name}' not the expected value: 'Test Function name'");
            Assert.True(functionResource.Uuid == guid, $"Function Resource UUId: '{functionResource.Uuid}' not the expected value: '{guid}'");
        }
    }
}
