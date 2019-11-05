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
using za.co.grindrodbank.a3s.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace za.co.grindrodbank.a3s.Repositories
{
    public class ApplicationFunctionRepository : IApplicationFunctionRepository
    {
        private readonly A3SContext a3SContext;

        public ApplicationFunctionRepository(A3SContext a3SContext)
        {
            this.a3SContext = a3SContext;
        }

        public void InitSharedTransaction()
        {
            if (a3SContext.Database.CurrentTransaction == null)
                a3SContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (a3SContext.Database.CurrentTransaction != null)
                a3SContext.Database.CurrentTransaction.Commit();
        }

        public void RollbackTransaction()
        {
            if (a3SContext.Database.CurrentTransaction != null)
                a3SContext.Database.CurrentTransaction.Rollback();
        }

        public async Task<ApplicationFunctionModel> CreateAsync(ApplicationFunctionModel applicationFunction)
        {
            a3SContext.ApplicationFunction.Add(applicationFunction);
            await a3SContext.SaveChangesAsync();

            return applicationFunction;
        }

        public async Task DeleteAsync(ApplicationFunctionModel applicationFunction)
        {
            a3SContext.ApplicationFunction.Remove(applicationFunction);
            await a3SContext.SaveChangesAsync();
        }

        public async Task<ApplicationFunctionModel> GetByIdAsync(Guid functionId)
        {
            return await a3SContext.ApplicationFunction.Where(f => f.Id == functionId)
                                            .Include(f => f.ApplicationFunctionPermissions)
                                              .ThenInclude(fp => fp.Permission)
                                            .FirstOrDefaultAsync();
        }

        public ApplicationFunctionModel GetByName(string name)
        {
            return a3SContext.ApplicationFunction.Where(f => f.Name == name)
                                            .Include(f => f.ApplicationFunctionPermissions)
                                              .ThenInclude(fp => fp.Permission)
                                            .FirstOrDefault();
        }

        public async Task<ApplicationFunctionModel> GetByNameAsync(string name)
        {
            return await a3SContext.ApplicationFunction.Where(f => f.Name == name)
                                            .Include(f => f.ApplicationFunctionPermissions)
                                              .ThenInclude(fp => fp.Permission)
                                            .FirstOrDefaultAsync();
        }

        public async Task<List<ApplicationFunctionModel>> GetListAsync()
        {
            return await a3SContext.ApplicationFunction.Include(f => f.ApplicationFunctionPermissions)
                                              .ThenInclude(fp => fp.Permission)
                                             .ToListAsync();
        }

        public async Task<ApplicationFunctionModel> UpdateAsync(ApplicationFunctionModel applicationFunction)
        {
            a3SContext.Entry(applicationFunction).State = EntityState.Modified;
            await a3SContext.SaveChangesAsync();

            return applicationFunction;
        }
    }
}
