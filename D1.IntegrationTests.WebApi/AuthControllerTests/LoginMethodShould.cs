using D1.IntegrationTests.WebApi.Seeds;
using D1.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApi.Model;
using Xunit;

namespace D1.IntegrationTests.WebApi.AuthControllerTests
{
    [Collection("Web Api Test Collection")]
    public  class LoginMethodShould
    {
        private readonly ApiSut _sut;

        public LoginMethodShould(ApiSut sut)
        {
            _sut = sut;
            _sut.SeedUsers();
        }

        [Fact]
        public async Task ReturnStatusMinusOneWhenLoginIsNull()
        {
            // Arrange
            var request = new LoginModel
            {
                Login = null,
                Password = "Password123"
            };
            var requestJson = JsonConvert.SerializeObject(request);
            var expectedResult = -1;

            // Action
            var response = await _sut.Client.PostAsync("/auth/login", new StringContent(requestJson, Encoding.UTF8, "application/json"));
            var resultJson = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResponse>(resultJson);

            // Assert
            Assert.Equal(expectedResult, result.Status);
        }
    }
}
