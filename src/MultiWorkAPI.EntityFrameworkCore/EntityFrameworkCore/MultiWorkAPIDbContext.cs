using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using MultiWorkAPI.Authorization.Roles;
using MultiWorkAPI.Authorization.Users;
using MultiWorkAPI.MultiTenancy;
using MultiWorkAPI.Brands;
using MultiWorkAPI.ProductGroups;
using MultiWorkAPI.Models;

namespace MultiWorkAPI.EntityFrameworkCore
{
    public class MultiWorkAPIDbContext : AbpZeroDbContext<Tenant, Role, User, MultiWorkAPIDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductGroup> ProductGroup { get; set; }
        public DbSet<ProductGroupBrand> ProductGroupBrand { get; set; }
        public DbSet<Model> Model{ get; set; }

        public MultiWorkAPIDbContext(DbContextOptions<MultiWorkAPIDbContext> options)
            : base(options)
        {
        }
    }
}
