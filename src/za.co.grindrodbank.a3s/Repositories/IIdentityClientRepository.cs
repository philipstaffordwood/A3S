/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;

namespace za.co.grindrodbank.a3s.Repositories
{
    public interface IIdentityClientRepository : ITransactableRepository
    {
        Task<Client> GetByClientIdAsync(string clientId);
        Task<Client> CreateAsync(Client client);
        Task<Client> UpdateAsync(Client client);
        Task<List<Client>> GetListAsync();
    }
}
