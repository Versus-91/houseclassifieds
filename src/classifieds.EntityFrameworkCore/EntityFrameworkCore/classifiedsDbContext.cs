using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using classifieds.Authorization.Roles;
using classifieds.Authorization.Users;
using classifieds.MultiTenancy;
using classifieds.Provinces;
using classifieds.Posts;
using classifieds.Cities;
using classifieds.Districts;
using classifieds.Categories;

namespace classifieds.EntityFrameworkCore
{
    public class classifiedsDbContext : AbpZeroDbContext<Tenant, Role, User, classifiedsDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<City> Citites { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public classifiedsDbContext(DbContextOptions<classifiedsDbContext> options)
            : base(options)
        {
        }
    }
}
