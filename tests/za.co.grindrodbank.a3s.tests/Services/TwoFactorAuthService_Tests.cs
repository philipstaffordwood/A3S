/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using za.co.grindrodbank.a3s.Exceptions;
using za.co.grindrodbank.a3s.Models;
using za.co.grindrodbank.a3s.Repositories;
using za.co.grindrodbank.a3s.Services;

namespace za.co.grindrodbank.a3s.tests.Services
{
    public class TwoFactorAuthService_Tests
    {
        [Fact]
        public async Task DeleteUser_GivenWrongGuid_ThrowsNotFoundException()
        {
            var userRepository = Substitute.For<IUserRepository>();
            var twoFactorAuthService = new TwoFactorAuthService(userRepository);

            Exception catchingException = null;
            try
            {
                await twoFactorAuthService.RemoveTwoFactorAuthenticationAsync(Guid.NewGuid());
            }
            catch (Exception ex)
            {
                catchingException = ex;
            }

            Assert.True(catchingException is ItemNotFoundException);
        }

        [Fact]
        public async Task DeleteUser_GivenEmptyGuid_ThrowsNotFoundException()
        {
            var userRepository = Substitute.For<IUserRepository>();
            var twoFactorAuthService = new TwoFactorAuthService(userRepository);

            Exception catchingException = null;
            try
            {
                await twoFactorAuthService.RemoveTwoFactorAuthenticationAsync(Guid.Empty);
            }
            catch (Exception ex)
            {
                catchingException = ex;
            }

            Assert.True(catchingException is ItemNotFoundException);
        }

        [Fact]
        public async Task DeleteUser_GivenCorrectGuid_ThrowsNoException()
        {
            var userRepository = Substitute.For<IUserRepository>();
            var twoFactorAuthService = new TwoFactorAuthService(userRepository);

            var testGuid = Guid.NewGuid();
            userRepository.GetByIdAsync(testGuid, Arg.Any<bool>()).Returns(new UserModel() { Id = testGuid.ToString() });
            await twoFactorAuthService.RemoveTwoFactorAuthenticationAsync(testGuid);

            Exception catchingException = null;
            try
            {
                await twoFactorAuthService.RemoveTwoFactorAuthenticationAsync(testGuid);
            }
            catch (Exception ex)
            {
                catchingException = ex;
                Assert.True(false);
            }

            Assert.True(catchingException is null);
        }
    }
}
