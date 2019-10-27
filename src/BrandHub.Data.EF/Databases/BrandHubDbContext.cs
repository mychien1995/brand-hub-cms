using BrandHub.Data.EF.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Data.EF.Databases
{
    public class BrandHubDbContext : DbContext
    {
        public BrandHubDbContext()
        {

        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<ApplicationUserRole> UserRoles { get; set; }
        public DbSet<ApplicationUserToken> UserTokens { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<HostDefinition> HostDefinitions { get; set; }
        public DbSet<OrganizationHost> OrganizationHosts { get; set; }
        public DbSet<OrganizationRole> OrganizationRoles { get; set; }
        public DbSet<OrganizationUser> OrganizationUsers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
    }
}
