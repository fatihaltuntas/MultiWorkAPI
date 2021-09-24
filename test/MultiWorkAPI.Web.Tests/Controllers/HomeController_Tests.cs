using System.Threading.Tasks;
using MultiWorkAPI.Models.TokenAuth;
using MultiWorkAPI.Web.Controllers;
using Shouldly;
using Xunit;

namespace MultiWorkAPI.Web.Tests.Controllers
{
    public class HomeController_Tests: MultiWorkAPIWebTestBase
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