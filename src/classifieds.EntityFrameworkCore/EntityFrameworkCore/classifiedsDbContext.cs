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
using classifieds.Reports;
using classifieds.ReportOptions;
using classifieds.UserNotificationIds;
using classifieds.Areas;
using classifieds.Favorites;
using classifieds.SaleReports;
using classifieds.RealEstates;

namespace classifieds.EntityFrameworkCore
{
    public class classifiedsDbContext : AbpZeroDbContext<Tenant, Role, User, classifiedsDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserNotificationId> UserNotificationIds { get; set; }

        public DbSet<Image> Images { get; set; }
        public DbSet<City> Citites { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        public DbSet<District> Districts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportOption> ReportOptions { get; set; }
        public DbSet<SaleReport> SaleReports { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }



        public DbSet<PostAmenity> PostsAmenities{ get; set; }
        public classifiedsDbContext(DbContextOptions<classifiedsDbContext> options)
            : base(options)
        {
        }
    }
}
