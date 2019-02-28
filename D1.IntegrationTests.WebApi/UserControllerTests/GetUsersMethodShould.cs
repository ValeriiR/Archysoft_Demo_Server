using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using D1.IntegrationTests.WebApi.Seeds;
using D1.Model;
using D1.Model.Authentification;
using D1.Model.Users;
using Newtonsoft.Json;
using WebApi.Model;
using Xunit;

namespace D1.IntegrationTests.WebApi.UserControllerTests
{
    [Collection("Web Api Test Collection")]
    public class GetUsersMethodShould
    {
        private readonly ApiSut _sut;

        public GetUsersMethodShould(ApiSut sut)
        {
            _sut = sut;
            _sut.SeedUsers();
        }

        [Fact]
        public async Task ReturnStatusCode401WhenUserIsUnauthorized()
        {
            //Arrange
            var expectedStatusCode = HttpStatusCode.Unauthorized;

            // Action
            _sut.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");

            var actualResult = await _sut.Client.GetAsync("/users", HttpCompletionOption.ResponseHeadersRead);

            // Assert
            Assert.Equal(expectedStatusCode, actualResult.StatusCode);
        }

        [Fact]
        public async Task ReturnStatusCode200WhenUserIsAuthrized()
        {
            //Arrange
            LoginModel loginModel = new LoginModel
            {
                Login = "integration.test@d1.archysoft.com",
                Password = "Pa$$w0rd1",
                RememberMe = false
            };

            Guid guidNumber = Guid.Parse("6bdbaf4b-72eb-49af-f6da-08d6855dadcf");

            UserGridModel userGridModel = new UserGridModel
            {
                Id = guidNumber,
                Email = "integration.test@d1.archysoft.com",
                UserName = "IntegrationTest"
            };
            List<UserGridModel> listUserGridModels = new List<UserGridModel>();
            listUserGridModels.Add(userGridModel);
           
            var requestJson = JsonConvert.SerializeObject(loginModel);

            var responseFromLogin = await _sut.Client.PostAsync("/auth/login", new StringContent(requestJson, Encoding.UTF8, "application/json"));
            var resultJson = await responseFromLogin.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResponse<TokenModel>>(resultJson);

            TokenModel tokenModel = result.Model;

            _sut.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenModel.AccessToken);

            //Action
            var responseFromUsers = await _sut.Client.GetAsync("/users", HttpCompletionOption.ResponseHeadersRead);

            //Assert
            Assert.Equal(HttpStatusCode.OK, responseFromUsers.StatusCode);
        }


      //  private 
    }
}
