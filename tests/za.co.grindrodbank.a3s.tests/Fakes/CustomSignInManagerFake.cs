/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using za.co.grindrodbank.a3s.Managers;
using za.co.grindrodbank.a3s.Models;
using za.co.grindrodbank.a3s.Repositories;
using za.co.grindrodbank.a3s.Services;

namespace za.co.grindrodbank.a3s.tests.Fakes
{
    public class CustomSignInManagerFake<TUser> : CustomSignInManager<TUser> where TUser : class
    {
        private bool signInSuccessful;
        private bool lockedOutResult;

        public CustomSignInManagerFake(UserManager<TUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<TUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<TUser>> logger, A3SContext a3SContext, IAuthenticationSchemeProvider authenticationSchemeProvider,
            ILdapAuthenticationModeRepository ldapAuthenticationModeRepository, ILdapConnectionService ldapConnectionService)
            : base (userManager, contextAccessor, claimsFactory, optionsAccessor, logger, a3SContext, authenticationSchemeProvider, ldapAuthenticationModeRepository, ldapConnectionService)
        {
        }

        public void SetSignInSuccessful(bool value)
        {
            signInSuccessful = value;
        }

        public void SetLockedOutState(bool value)
        {
            lockedOutResult = value;
        }

        public override Task<SignInResult> PasswordSignInAsync(string username, string password, bool isPersistent, bool lockoutOnFailure)
        {
            if (lockedOutResult)
                return Task.FromResult(SignInResult.LockedOut);

            if (signInSuccessful)
                return Task.FromResult(SignInResult.Success);
            else
                return Task.FromResult(SignInResult.Failed);
        }
    }
}
