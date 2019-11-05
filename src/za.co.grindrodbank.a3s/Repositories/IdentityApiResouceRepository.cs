/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace za.co.grindrodbank.a3s.Repositories
{
    public class IdentityApiResouceRepository : IIdentityApiResourceRepository
    {
        private readonly ConfigurationDbContext identityServerConfigurationContext;
        private readonly IMapper mapper;

        public IdentityApiResouceRepository(ConfigurationDbContext identityServerConfigurationContext, IMapper mapper)
        {
            this.identityServerConfigurationContext = identityServerConfigurationContext;
            this.mapper = mapper;
        }

        public void InitSharedTransaction()
        {
            if (identityServerConfigurationContext.Database.CurrentTransaction == null)
                identityServerConfigurationContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (identityServerConfigurationContext.Database.CurrentTransaction != null)
                identityServerConfigurationContext.Database.CurrentTransaction.Commit();
        }

        public void RollbackTransaction()
        {
            if (identityServerConfigurationContext.Database.CurrentTransaction != null)
                identityServerConfigurationContext.Database.CurrentTransaction.Rollback();
        }

        public async Task<ApiResource> CreateAsync(string name, string[] userClaims)
        {
            IdentityServer4.Models.ApiResource apiResourceModel = new IdentityServer4.Models.ApiResource(name, name, userClaims);
            // Need to use our own mapper definition as there is a conflict with the AutoMapper used in the included IDS4 package.
            ApiResource apiResource = mapper.Map<ApiResource>(apiResourceModel);

            identityServerConfigurationContext.ApiResources.Add(apiResource);
            await identityServerConfigurationContext.SaveChangesAsync();

            return apiResource;
        }

        public async Task<ApiResource> GetByNameAsync(string name)
        {
            return await identityServerConfigurationContext.ApiResources.Where(r => r.Name == name).FirstOrDefaultAsync();
        }

        public async Task<List<ApiResource>> GetListAsync()
        {
            return await identityServerConfigurationContext.ApiResources.ToListAsync(); 
        }
    }
}
