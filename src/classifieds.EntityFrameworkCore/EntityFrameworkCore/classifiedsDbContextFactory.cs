using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using classifieds.Configuration;
using classifieds.Web;

namespace classifieds.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class classifiedsDbContextFactory : IDesignTimeDbContextFactory<classifiedsDbContext>
    {
        public classifiedsDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<classifiedsDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            classifiedsDbContextConfigurer.Configure(builder, configuration.GetConnectionString(classifiedsConsts.ConnectionStringName));

            return new classifiedsDbContext(builder.Options);
        }
    }
}
