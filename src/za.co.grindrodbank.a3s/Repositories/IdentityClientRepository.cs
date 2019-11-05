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
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace za.co.grindrodbank.a3s.Repositories
{
    public class IdentityClientRepository : IIdentityClientRepository
    {
        private readonly ConfigurationDbContext identityServerConfigurationContext;

        public IdentityClientRepository(ConfigurationDbContext identityServerConfigurationContext)
        {
            this.identityServerConfigurationContext = identityServerConfigurationContext;
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

        public async Task<Client> CreateAsync(Client client)
        {
            identityServerConfigurationContext.Clients.Add(client);
            await identityServerConfigurationContext.SaveChangesAsync();

            return client;
        }

        public async Task<Client> GetByClientIdAsync(string clientId)
        {
            return await identityServerConfigurationContext.Clients.Where(c => c.ClientId == clientId)
                                                                   .Include(c => c.ClientSecrets)
                                                                   .Include(c => c.AllowedCorsOrigins)
                                                                   .Include(c => c.AllowedScopes)
                                                                   .Include(c => c.PostLogoutRedirectUris)
                                                                   .Include(c => c.RedirectUris)
                                                                   .Include(c => c.AllowedGrantTypes)
                                                                   .FirstOrDefaultAsync();
        }

        public async Task<List<Client>> GetListAsync()
        {
            return await identityServerConfigurationContext.Clients.Include(c => c.ClientSecrets)
                                                                   .Include(c => c.AllowedCorsOrigins)
                                                                   .Include(c => c.AllowedScopes)
                                                                   .Include(c => c.PostLogoutRedirectUris)
                                                                   .Include(c => c.RedirectUris)
                                                                   .Include(c => c.AllowedGrantTypes)
                                                                   .ToListAsync();
        }

        public async Task<Client> UpdateAsync(Client client)
        {
            identityServerConfigurationContext.Entry(client).State = EntityState.Modified;
            await identityServerConfigurationContext.SaveChangesAsync();

            return client;
        }
    }
}
