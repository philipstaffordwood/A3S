/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
using System;
using za.co.grindrodbank.a3s.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace za.co.grindrodbank.a3s.Models
{
    public class A3SContext : DbContext
    {
        public A3SContext(DbContextOptions<A3SContext> options) : base(options)
        {
        }

        public DbSet<UserModel> User { get; set; }
        public DbSet<UserTokenModel> UserToken { get; set; }
        public DbSet<ApplicationModel> Application { get; set; }
        public DbSet<PermissionModel> Permission { get; set; }
        public DbSet<RoleModel> Role { get; set; }
        public DbSet<TeamModel> Team { get; set; }
        public DbSet<UserRoleModel> UserRole { get; set; }
        public DbSet<UserTeamModel> UserTeam { get; set; }
        public DbSet<FunctionModel> Function { get; set; }
        public DbSet<FunctionPermissionModel> FunctionPermission { get; set; }
        public DbSet<ApplicationFunctionModel> ApplicationFunction { get; set; }
        public DbSet<ApplicationFunctionPermissionModel> ApplicationFunctionPermission { get; set; }
        public DbSet<RoleFunctionModel> RoleFunction { get; set; }
        public DbSet<LdapAuthenticationModeModel> LdapAuthenticationMode { get; set; }
        public DbSet<LdapAuthenticationModeLdapAttributeModel> LdapAuthenticationModeLdapAttribute { get; set; }
        public DbSet<ApplicationDataPolicyModel> ApplicationDataPolicy { get; set; }
        public DbSet<TermsOfServiceModel> TermsOfService { get; set; }

        // Identity specific database tables. We want to operate on these, but let them be managed by Identity.
        public DbSet<UserClaimModel> ApplicationUserClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("_a3s");

            // Rename AspNet Identity tables
            modelBuilder.Entity<UserModel>().ToTable("application_user");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("application_user_claim");
            modelBuilder.Entity<IdentityRole>().ToTable("aspnet_role");

            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("aspnet_user_role")
                .HasKey(o => o.RoleId);

            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("aspnet_role_claim");

            modelBuilder.Entity<UserTokenModel>().ToTable("application_user_token")
                .HasKey(o => new { o.UserId, o.LoginProvider, o.Name });

            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("aspnet_user_login")
                .HasKey(o => new { o.LoginProvider, o.ProviderKey });

            // Add uniqueness contraint to Role Name
            modelBuilder.Entity<RoleModel>()
                .HasIndex(r => r.Name)
                .IsUnique();

            // Customisations for many to many relationship between functions and permissions.
            modelBuilder.Entity<FunctionPermissionModel>()
                .HasKey(fp => new { fp.PermissionId, fp.FunctionId });

            modelBuilder.Entity<FunctionPermissionModel>()
                .HasOne(fp => fp.Function)
                .WithMany(f => f.FunctionPermissions)
                .HasForeignKey(fp => fp.FunctionId);

            modelBuilder.Entity<FunctionPermissionModel>()
                .HasOne(fp => fp.Permission)
                .WithMany(p => p.FunctionPermissions)
                .HasForeignKey(fp => fp.PermissionId);

            // Customisations for many to many relationship between application functions and permissions.
            modelBuilder.Entity<ApplicationFunctionPermissionModel>()
                .HasKey(fp => new { fp.PermissionId, fp.ApplicationFunctionId });

            modelBuilder.Entity<ApplicationFunctionPermissionModel>()
                .HasOne(fp => fp.ApplicationFunction)
                .WithMany(f => f.ApplicationFunctionPermissions)
                .HasForeignKey(fp => fp.ApplicationFunctionId);

            modelBuilder.Entity<ApplicationFunctionPermissionModel>()
                .HasOne(fp => fp.Permission)
                .WithMany(p => p.ApplicationFunctionPermissions)
                .HasForeignKey(fp => fp.PermissionId);

            // Customisations for many to many relationships between roles and functions.
            modelBuilder.Entity<RoleFunctionModel>()
                .HasKey(rf => new { rf.RoleId, rf.FunctionId });

            modelBuilder.Entity<RoleFunctionModel>()
                .HasOne(rf => rf.Function)
                .WithMany(f => f.RoleFunctions)
                .HasForeignKey(rf => rf.FunctionId);

            modelBuilder.Entity<RoleFunctionModel>()
                .HasOne(rf => rf.Role)
                .WithMany(r => r.RoleFunctions)
                .HasForeignKey(rf => rf.RoleId);

            // Customisations for many to many relationships between users and roles.
            modelBuilder.Entity<UserRoleModel>()
                .HasKey(ur => new { ur.RoleId, ur.UserId });

            modelBuilder.Entity<UserRoleModel>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRoleModel>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // Customisations for many to many relationships between users and teams.
            modelBuilder.Entity<UserTeamModel>()
                .HasKey(ut => new { ut.TeamId, ut.UserId });

            modelBuilder.Entity<UserTeamModel>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.UserTeams)
                .HasForeignKey(ut => ut.UserId);

            modelBuilder.Entity<UserTeamModel>()
                .HasOne(ut => ut.Team)
                .WithMany(t => t.UserTeams)
                .HasForeignKey(ut => ut.TeamId);


            // Customisations for many to many relationships between teams and teams.
            modelBuilder.Entity<TeamTeamModel>()
                .HasKey(tt => new { tt.ParentTeamId, tt.ChildTeamId });

            modelBuilder.Entity<TeamTeamModel>()
                .HasOne(tt => tt.ParentTeam)
                .WithMany(t => t.ChildTeams)
                .HasForeignKey(tt => tt.ParentTeamId);

            modelBuilder.Entity<TeamTeamModel>()
                .HasOne(tt => tt.ChildTeam)
                .WithMany(t => t.ParentTeams)
                .HasForeignKey(tt => tt.ChildTeamId);

            // Customisations for many to many relationships between roles and roles.
            modelBuilder.Entity<RoleRoleModel>()
                .HasKey(rr => new { rr.ParentRoleId, rr.ChildRoleId });

            modelBuilder.Entity<RoleRoleModel>()
                .HasOne(rr => rr.ParentRole)
                .WithMany(r => r.ChildRoles)
                .HasForeignKey(rr => rr.ParentRoleId);

            modelBuilder.Entity<RoleRoleModel>()
                .HasOne(rr => rr.ChildRole)
                .WithMany(r => r.ParentRoles)
                .HasForeignKey(rr => rr.ChildRoleId);

            // Customisations for one to many relationship between users and LdapAuthModes
            modelBuilder.Entity<UserModel>()
                .HasOne(l => l.LdapAuthenticationMode)
                .WithMany(u => u.Users)
                .HasForeignKey(l => l.LdapAuthenticationModeId);

            modelBuilder.Entity<UserTokenModel>()
                .HasOne(t => t.User)
                .WithMany(u => u.UserTokens)
                .HasForeignKey(t => t.UserId);

            // Customisations for one to many relationship between LdapAuthenticationModes and LdapAuthenticationModeLdapAttributes
            modelBuilder.Entity<LdapAuthenticationModeLdapAttributeModel>()
                        .Property(f => f.Id)
                        .ValueGeneratedOnAdd();

            modelBuilder.Entity<LdapAuthenticationModeModel>()
                .HasMany(att => att.LdapAttributes)
                .WithOne(auth => auth.LdapAuthenticationMode)
                .HasForeignKey(l => l.LdapAuthenticationModeId);

            // Customisations for many to many relationships between teams and application data policies.
            modelBuilder.Entity<TeamApplicationDataPolicyModel>()
                .HasKey(tdp => new { tdp.TeamId, tdp.ApplicationDataPolicyId });

            modelBuilder.Entity<TeamApplicationDataPolicyModel>()
                .HasOne(tdp => tdp.Team)
                .WithMany(t => t.ApplicationDataPolicies)
                .HasForeignKey(tdp => tdp.TeamId);

            modelBuilder.Entity<TeamApplicationDataPolicyModel>()
                .HasOne(tdp => tdp.ApplicationDataPolicy)
                .WithMany(adp => adp.ApplicationDataPolicies)
                .HasForeignKey(tdp => tdp.ApplicationDataPolicyId);

            // Customisations for one to many relationship between TermsOfService and Teams
            modelBuilder.Entity<TeamModel>()
                .HasOne(t => t.TermsOfService)
                .WithMany(tOS => tOS.Teams)
                .HasForeignKey(t => t.TermsOfServiceId);

            SetDbNamingConvention(modelBuilder);
            SetSysPeriodTransformedColumns(modelBuilder);

            modelBuilder.Entity<ApplicationModel>()
                .Property(p => p.SysPeriod)
                .HasDefaultValueSql("GetUtcDate()");

        }

        public void SetDbNamingConvention(ModelBuilder modelBuilder)
        {
            // Convert each entity to snake case for database name
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.Relational().TableName.ToSnakeCase();

                foreach (var property in entity.GetProperties())
                    property.Relational().ColumnName = property.Name.ToSnakeCase();

                foreach (var key in entity.GetKeys())
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();

                foreach (var key in entity.GetForeignKeys())
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();

                foreach (var index in entity.GetIndexes())
                    index.Relational().Name = index.Relational().Name.ToSnakeCase();
            }
        }

        public void SetSysPeriodTransformedColumns(ModelBuilder modelBuilder)
        {
            var entityList = new List<IMutableEntityType>();

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var entType = Type.GetType(entity.Name);

                if (entType != null)
                {
                    if (typeof(AuditableModel).GetTypeInfo().IsAssignableFrom(entType.GetTypeInfo()) ||
                        typeof(IdentityUser).GetTypeInfo().IsAssignableFrom(entType.GetTypeInfo()))
                    {
                        var property = entity.GetProperties().FirstOrDefault(x => x.Name == "SysPeriod");

                        if (property != null)
                        {
                            property.Relational().ColumnType = "tstzrange";
                            property.Relational().DefaultValueSql = "tstzrange(current_timestamp, null)";
                        }
                    }
                }
            }
        }
    }
}
