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

namespace za.co.grindrodbank.a3s.tests.Services
{
    public class AuthenticationModeService_Tests
    {
        IMapper mapper;
        Guid userGuid;
        Guid authenticationModeGuid;

        LdapAuthenticationModeModel mockedAuthenticationMode;

        public AuthenticationModeService_Tests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new LdapAuthenticationModeResourceLdapAuthenticationModeModelProfile());
            });

            mapper = config.CreateMapper();
            authenticationModeGuid = Guid.NewGuid();
            userGuid = Guid.NewGuid();

            mockedAuthenticationMode = new LdapAuthenticationModeModel();
            mockedAuthenticationMode.Id = authenticationModeGuid;
            mockedAuthenticationMode.Name = "Test AuthenticationMode Name";
            mockedAuthenticationMode.Account = "TestAccount";
            mockedAuthenticationMode.BaseDn = "TestBaseDN";
            mockedAuthenticationMode.HostName = "TestHostName";
            mockedAuthenticationMode.IsLdaps = true;
            mockedAuthenticationMode.Password = "TestPass";
            mockedAuthenticationMode.Port = 389;
        }

        [Fact]
        public async Task GetById_GivenGuid_ReturnsAuthenticationModeResource()
        {
            var userRepository = Substitute.For<IUserRepository>();
            var authenticationModeRepository = Substitute.For<ILdapAuthenticationModeRepository>();
            var ldapConnectionService = Substitute.For<ILdapConnectionService>();

            authenticationModeRepository.GetByIdAsync(authenticationModeGuid).Returns(mockedAuthenticationMode);

            var authenticationModeService = new LdapAuthenticationModeService(authenticationModeRepository, mapper, ldapConnectionService);
            var authenticationModeResource = await authenticationModeService.GetByIdAsync(authenticationModeGuid);

            Assert.True(authenticationModeResource.Name == mockedAuthenticationMode.Name, $"Expected authenticationMode name: '{authenticationModeResource.Name}' does not equal expected value: '{mockedAuthenticationMode.Name}'");
            Assert.True(authenticationModeResource.Uuid == authenticationModeGuid, $"Expected authenticationMode UUID: '{authenticationModeResource.Uuid}' does not equal expected value: '{authenticationModeGuid}'");
            Assert.True(authenticationModeResource.Account == mockedAuthenticationMode.Account, $"Expected authenticationMode name: '{authenticationModeResource.Account}' does not equal expected value: '{mockedAuthenticationMode.Account}'");
            Assert.True(authenticationModeResource.BaseDn == mockedAuthenticationMode.BaseDn, $"Expected authenticationMode name: '{authenticationModeResource.BaseDn}' does not equal expected value: '{mockedAuthenticationMode.BaseDn}'");
            Assert.True(authenticationModeResource.HostName == mockedAuthenticationMode.HostName, $"Expected authenticationMode name: '{authenticationModeResource.HostName}' does not equal expected value: '{mockedAuthenticationMode.HostName}'");
            Assert.True(authenticationModeResource.IsLdaps == mockedAuthenticationMode.IsLdaps, $"Expected authenticationMode name: '{authenticationModeResource.IsLdaps}' does not equal expected value: '{mockedAuthenticationMode.IsLdaps}'");
            Assert.True(authenticationModeResource.Port == mockedAuthenticationMode.Port, $"Expected authenticationMode name: '{authenticationModeResource.Port}' does not equal expected value: '{mockedAuthenticationMode.Port}'");
        }
    }
}
