/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;

namespace za.co.grindrodbank.a3s.Repositories
{
    public interface IIdentityApiResourceRepository : ITransactableRepository
    {
        Task<ApiResource> GetByNameAsync(string name);
        Task<ApiResource> CreateAsync(string name, string[] userClaims);
        Task<List<ApiResource>> GetListAsync();
    }
}
