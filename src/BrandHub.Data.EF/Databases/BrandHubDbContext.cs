using BrandHub.Data.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BrandHub.Data.EF.Databases
{
    public class BrandHubDbContext : DbContext
    {
        public BrandHubDbContext()
        {

        }
        public BrandHubDbContext(DbContextOptions<BrandHubDbContext> options)
    : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("Default");
                optionsBuilder.UseSqlServer(connectionString).UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<ApplicationRole>().ToTable("Roles");
            modelBuilder.Entity<ApplicationUserToken>().ToTable("UserTokens");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<ApplicationUser>
            (
                x =>
                {
                    x.HasMany(c => c.UserRoles).WithOne(c => c.User).HasForeignKey(c => c.UserId).IsRequired(true);
                    x.HasMany(c => c.OrganizationUserRoles).WithOne(c => c.User).HasForeignKey(c => c.UserId).IsRequired(true);
                }
            );
            modelBuilder.Entity<Organization>
            (
                x =>
                {
                    x.HasMany(c => c.WebsiteHosts).WithOne(c => c.Organization).HasForeignKey(c => c.OrganizationId).IsRequired(true);
                    x.HasMany(c => c.OrganizationUserRoles).WithOne(c => c.Organization).HasForeignKey(c => c.OrganizationId).IsRequired(true);
                }
            );
            modelBuilder.Entity<Province>().HasOne(p => p.Country).WithMany(x => x.Provinces).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<District>().HasOne(p => p.Province).WithMany(x => x.Districts).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Address>().HasOne(p => p.Country).WithMany(x => x.Addresses).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Address>().HasOne(p => p.District).WithMany(x => x.Addresses).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Address>().HasOne(p => p.Province).WithMany(x => x.Addresses).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OrganizationUser>().HasOne(p => p.Organization).WithMany(x => x.OrganizationUserRoles).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OrganizationUser>().HasOne(p => p.User).WithMany(x => x.OrganizationUserRoles).OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<ApplicationUserRole> UserRoles { get; set; }
        public DbSet<ApplicationUserToken> UserTokens { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<HostDefinition> HostDefinitions { get; set; }
        public DbSet<OrganizationRole> OrganizationRoles { get; set; }
        public DbSet<OrganizationUser> OrganizationUsers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
    }
}
