using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using MultiWorkAPI.Authorization.Roles;
using MultiWorkAPI.Authorization.Users;
using MultiWorkAPI.MultiTenancy;
using MultiWorkAPI.Brands;

namespace MultiWorkAPI.EntityFrameworkCore
{
    public class MultiWorkAPIDbContext : AbpZeroDbContext<Tenant, Role, User, MultiWorkAPIDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Brand> Brands { get; set; }

        public MultiWorkAPIDbContext(DbContextOptions<MultiWorkAPIDbContext> options)
            : base(options)
        {
        }
    }
}
