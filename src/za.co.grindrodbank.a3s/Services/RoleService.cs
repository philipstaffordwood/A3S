/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using za.co.grindrodbank.a3s.Exceptions;
using za.co.grindrodbank.a3s.Models;
using za.co.grindrodbank.a3s.Repositories;
using AutoMapper;
using NLog;
using za.co.grindrodbank.a3s.A3SApiResources;

namespace za.co.grindrodbank.a3s.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;
        private readonly IUserRepository userRepository;
        private readonly IFunctionRepository functionRepository;

        private readonly IMapper mapper;
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public RoleService(IRoleRepository roleRepository, IUserRepository userRepository, IFunctionRepository functionRepository, IMapper mapper)
        {
            this.roleRepository = roleRepository;
            this.userRepository = userRepository;
            this.functionRepository = functionRepository;
            this.mapper = mapper;
        }

        public async Task<Role> CreateAsync(RoleSubmit roleSubmit, Guid createdById)
        {
            // Start transactions to allow complete rollback in case of an error
            BeginAllTransactions();

            try
            {
                RoleModel existingRole = await roleRepository.GetByNameAsync(roleSubmit.Name);
                if (existingRole != null)
                    throw new ItemNotProcessableException($"Role with Name '{roleSubmit.Name}' already exist.");

                // Note: The mapper will only map the basic first level members of the RoleSubmit to the Role.
                // The RoleSubmit contains a list of User UUIDs that will need to be found and converted into actual user representations.
                RoleModel newRole = mapper.Map<RoleModel>(roleSubmit);
                newRole.ChangedBy = createdById;

                await AssignFunctionsToRoleFromFunctionIdList(newRole, roleSubmit.FunctionIds);
                await AssignRolesToRoleFromRolesIdList(newRole, roleSubmit.RoleIds);

                // All successful
                CommitAllTransactions();

                return mapper.Map<Role>(await roleRepository.CreateAsync(newRole));
            }
            catch
            {
                RollbackAllTransactions();
                throw;
            }
        }

        public async Task<Role> GetByIdAsync(Guid roleId)
        {
            return mapper.Map<Role>(await roleRepository.GetByIdAsync(roleId));
        }

        public async Task<List<Role>> GetListAsync()
        {
            return mapper.Map<List<Role>>(await roleRepository.GetListAsync());
        }

        public async Task<Role> UpdateAsync(RoleSubmit roleSubmit, Guid updatedById)
        {
            // Start transactions to allow complete rollback in case of an error
            BeginAllTransactions();

            try
            {
                // Note: The mapper will only map the basic first level members of the RoleSubmit to the Role.
                // The RoleSubmit contains a list of User UUIDs that will need to be found and converted into actual user representations.
                RoleModel role = await roleRepository.GetByIdAsync(roleSubmit.Uuid);

                if(role == null)
                    throw new ItemNotFoundException($"Role with ID '{roleSubmit.Uuid}' not found when attempting to update a role using this ID!");

                if (role.Name != roleSubmit.Name)
                {
                    // Confirm the new name is available
                    var checkExistingNameModel = await roleRepository.GetByNameAsync(roleSubmit.Name);
                    if (checkExistingNameModel != null)
                        throw new ItemNotProcessableException($"Role with name '{roleSubmit.Name}' already exists.");
                }

                role.Name = roleSubmit.Name;
                role.Description = roleSubmit.Description;
                role.ChangedBy = updatedById;

                await AssignFunctionsToRoleFromFunctionIdList(role, roleSubmit.FunctionIds);
                await AssignRolesToRoleFromRolesIdList(role, roleSubmit.RoleIds);

                // All successful
                CommitAllTransactions();

                return mapper.Map<Role>(await roleRepository.UpdateAsync(role));
            }
            catch
            {
                RollbackAllTransactions();
                throw;
            }
        }

        private async Task AssignFunctionsToRoleFromFunctionIdList(RoleModel role, List<Guid> functionIds)
        {
            // The user associations for this role are going to be created or overwritten, its easier to rebuild it that apply a diff.
            role.RoleFunctions = new List<RoleFunctionModel>();

            if (functionIds != null && functionIds.Count > 0)
            {
                foreach (var functionId in functionIds)
                {
                    var function = await functionRepository.GetByIdAsync(functionId);

                    if (function == null)
                    {
                        throw new ItemNotFoundException("Unable to find a function with ID: " + functionId + "when attempting to assign it to a role.");
                    }

                    role.RoleFunctions.Add(new RoleFunctionModel
                    {
                        Role = role,
                        Function = function
                    });
                }
            }
        }

        /// <summary>
        /// Assigns child roles to a role from a List of child role IDs. This methid will check that there is a legitimate role associated with each supplied
        /// Role ID within the list.
        /// </summary>
        /// <param name="roleModel"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        private async Task AssignRolesToRoleFromRolesIdList(RoleModel roleModel, List<Guid> roleIds)
        {
            // Child Roles are not mandatory. If the role IDs are null, return without resetting their state
            if(roleIds == null)
            {
                logger.Warn($"Role IDs are null. Returning.");
                return;
            }

            // If the child roles element is set, reset the association list, even if there are no elements in it.
            roleModel.ChildRoles = new List<RoleRoleModel>();

            if(roleIds.Count == 0)
            {
                logger.Warn($"Role IDs list is empty. Returning.");
                return;
            }

            foreach(var roleIdToAddAsChildRole in roleIds)
            {
                var roleToAddAsChildRole = await roleRepository.GetByIdAsync(roleIdToAddAsChildRole);

                if(roleToAddAsChildRole == null)
                {
                    throw new ItemNotFoundException($"Unable to find role with ID: '{roleIdToAddAsChildRole}' when attempting to assign this role as a child of role: '{roleModel.Name}'.");
                }

                // Only non-compound roles can be added to compound roles. Therefore, prior to adding the potential child role to the parent role, it must be
                // asserted that the child row has no child roles attached to it.
                if(roleToAddAsChildRole.ChildRoles.Count > 0)
                {
                    // Note. This function is called by create role and update role functions within this class. Therefore, the 'roleModel' object will not have an ID set if called from the create context. Use it's name.
                    throw new ItemNotProcessableException($"Assigning a compound role as a child of a role is prohibited. Attempting to add Role '{roleToAddAsChildRole.Name} with ID: '{roleToAddAsChildRole.Id}' as a child role of Role: '{roleModel.Name}'. However, it already has '{roleToAddAsChildRole.ChildRoles.Count}' child roles assigned to it! Not adding it.");
                }

                roleModel.ChildRoles.Add(new RoleRoleModel {
                    ParentRole = roleModel,
                    ChildRole = roleToAddAsChildRole
                });
            }
        }

        private void BeginAllTransactions()
        {
            userRepository.InitSharedTransaction();
            roleRepository.InitSharedTransaction();
            functionRepository.InitSharedTransaction();
        }

        private void CommitAllTransactions()
        {
            userRepository.CommitTransaction();
            roleRepository.CommitTransaction();
            functionRepository.CommitTransaction();
        }

        private void RollbackAllTransactions()
        {
            userRepository.RollbackTransaction();
            roleRepository.RollbackTransaction();
            functionRepository.RollbackTransaction();
        }
    }
}
