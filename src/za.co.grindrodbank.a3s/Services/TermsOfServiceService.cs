/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using NLog;
using za.co.grindrodbank.a3s.A3SApiResources;
using za.co.grindrodbank.a3s.Exceptions;
using za.co.grindrodbank.a3s.Models;
using za.co.grindrodbank.a3s.Repositories;

namespace za.co.grindrodbank.a3s.Services
{
    public class TermsOfServiceService : ITermsOfServiceService
    {
        private readonly ITermsOfServiceRepository termsOfServiceRepository;
        private readonly IMapper mapper;
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public TermsOfServiceService(ITermsOfServiceRepository termsOfServiceRepository, IMapper mapper)
        {
            this.termsOfServiceRepository = termsOfServiceRepository;
            this.mapper = mapper;
        }

        public async Task<TermsOfService> CreateAsync(TermsOfServiceSubmit termsOfServiceSubmit, Guid createdById)
        {
            // Start transactions to allow complete rollback in case of an error
            BeginAllTransactions();

            try
            {
                // This will only map the first level of members onto the model. User IDs and Policy IDs will not be.
                var termsOfServiceModel = mapper.Map<TermsOfServiceModel>(termsOfServiceSubmit);
                termsOfServiceModel.ChangedBy = createdById;
                termsOfServiceModel.Version = "1";

                // TODO: Implement mass teams update if AutoUpdate == true

                var createdTermsOfService = mapper.Map<TermsOfService>(await termsOfServiceRepository.CreateAsync(termsOfServiceModel));

                // All successful
                CommitAllTransactions();

                return createdTermsOfService;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                RollbackAllTransactions();
                throw;
            }
        }

        public async Task DeleteAsync(Guid termsOfServiceId)
        {
            var termsOfService = await termsOfServiceRepository.GetByIdAsync(termsOfServiceId, false);

            if (termsOfService == null)
                throw new ItemNotFoundException($"Terms of Service entry with GUID '{termsOfServiceId}' not found.");

            // TODO: Implement user acceptance check before delete

            await termsOfServiceRepository.DeleteAsync(termsOfService);
        }

        public async Task<TermsOfService> GetByIdAsync(Guid termsOfServiceId, bool includeRelations = false)
        {
            return mapper.Map<TermsOfService>(await termsOfServiceRepository.GetByIdAsync(termsOfServiceId, includeRelations));
        }

        public async Task<List<TermsOfService>> GetListAsync()
        {
            return mapper.Map<List<TermsOfService>>(await termsOfServiceRepository.GetListAsync());
        }

        private void BeginAllTransactions()
        {
            termsOfServiceRepository.InitSharedTransaction();
        }

        private void CommitAllTransactions()
        {
            termsOfServiceRepository.CommitTransaction();
        }

        private void RollbackAllTransactions()
        {
            termsOfServiceRepository.RollbackTransaction();
        }

    }
}
