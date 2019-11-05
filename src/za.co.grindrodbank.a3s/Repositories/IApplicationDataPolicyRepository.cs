/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using za.co.grindrodbank.a3s.Models;

namespace za.co.grindrodbank.a3s.Repositories
{
    public interface IApplicationDataPolicyRepository : ITransactableRepository
    {
        Task<ApplicationDataPolicyModel> GetByIdAsync(Guid applicationDataPolicyId);
        Task<ApplicationDataPolicyModel> GetByNameAsync(string name);
        Task<ApplicationDataPolicyModel> CreateAsync(ApplicationDataPolicyModel applicationDataPolicy);
        Task<ApplicationDataPolicyModel> UpdateAsync(ApplicationDataPolicyModel applicationDataPolicy);
        Task DeleteAsync(ApplicationDataPolicyModel applicationDataPolicy);
        Task<List<ApplicationDataPolicyModel>> GetListAsync();
    }
}
