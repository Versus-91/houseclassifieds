using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using classifieds.Authorization.Roles;
using classifieds.Authorization.Users;
using classifieds.MultiTenancy;
using classifieds.Posts;
using classifieds.Cities;
using classifieds.Districts;
using classifieds.Categories;
using classifieds.Images;
using classifieds.PropertyTypes;
using classifieds.Amenities;
using classifieds.PostsAmenities;

namespace classifieds.EntityFrameworkCore
{
    public class classifiedsDbContext : AbpZeroDbContext<Tenant, Role, User, classifiedsDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Post> Posts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<City> Citites { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<PostAmenity> PostsAmenities{ get; set; }
        public classifiedsDbContext(DbContextOptions<classifiedsDbContext> options)
            : base(options)
        {
        }
    }
}
