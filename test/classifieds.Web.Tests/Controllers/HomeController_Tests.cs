using System.Threading.Tasks;
using classifieds.Models.TokenAuth;
using classifieds.Web.Controllers;
using Shouldly;
using Xunit;

namespace classifieds.Web.Tests.Controllers
{
    public class HomeController_Tests: classifiedsWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}