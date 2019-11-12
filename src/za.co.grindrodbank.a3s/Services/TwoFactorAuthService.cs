/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NLog;
using za.co.grindrodbank.a3s.Exceptions;
using za.co.grindrodbank.a3s.Extensions;
using za.co.grindrodbank.a3s.Models;
using za.co.grindrodbank.a3s.Repositories;

namespace za.co.grindrodbank.a3s.Services
{
    public class TwoFactorAuthService : ITwoFactorAuthService
    {
        private readonly IUserRepository userRepository;
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly TokenOptions tokenOptions = new TokenOptions();

        public TwoFactorAuthService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task RemoveTwoFactorAuthenticationAsync(Guid userId)
        {
            // Start transactions to allow complete rollback in case of an error
            BeginAllTransactions();

            try
            {
                UserModel userModel = await userRepository.GetByIdAsync(userId, true);
                if (userModel == null)
                    throw new ItemNotFoundException($"User with Id '{userId}' not found when attempting to update a user using this Id.");

                // Remove all two-factor references and tokens
                userModel.TwoFactorEnabled = false;

                if (userModel.UserTokens != null)
                {
                    userModel.UserTokens.RemoveAll(x =>
                            x.Name == tokenOptions.GetAuthenticatorKeyName() ||
                            x.Name == tokenOptions.GetRecoverCodesName()
                        );
                }

                UserModel updatedUser = await userRepository.UpdateAsync(userModel);

                // All successful
                CommitAllTransactions();
             }
            catch (Exception ex)
            {
                logger.Error(ex);
                RollbackAllTransactions();
                throw;
            }
        }

        private void BeginAllTransactions()
        {
            userRepository.InitSharedTransaction();
        }

        private void CommitAllTransactions()
        {
            userRepository.CommitTransaction();
        }

        private void RollbackAllTransactions()
        {
            userRepository.RollbackTransaction();
        }
    }
}
