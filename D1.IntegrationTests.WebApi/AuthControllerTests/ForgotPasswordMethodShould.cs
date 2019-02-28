using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using D1.IntegrationTests.WebApi.Seeds;
using D1.Model.Authentification;
using Newtonsoft.Json;
using WebApi.Model;
using Xunit;

namespace D1.IntegrationTests.WebApi.AuthControllerTests
{
    [Collection("Web Api Test Collection")]
    public class ForgotPasswordMethodShould
    {
        private readonly ApiSut _sut;

        public ForgotPasswordMethodShould(ApiSut sut)
        {
            _sut = sut;
            _sut.SeedUsers();
        }

        [Fact]
        public async Task ReturnStatusMinusOneWhenUserIsNotFoundByEmailAdress()
        {
            //Arrange
            int expectedResult = -2;         
            ForgotPasswordModel forgotPasswordModel = new ForgotPasswordModel
                { Email = "email@gmail.com" };

            var requestJson = JsonConvert.SerializeObject(forgotPasswordModel);
            //Action
            var response = await _sut.Client.PostAsync("/auth/forgot-password", new StringContent(requestJson, Encoding.UTF8, "application/json"));          
            var resultJson = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResponse>(resultJson);

            // Assert
            Assert.Equal(expectedResult, result.Status);
        }

        [Fact]
        public void ReturnStatusOneWhenUserIsFoundByEmailAdressAndEmailIsSent()
        {
            //Arrange
            int expectedResult = 1;
            ForgotPasswordModel forgotPasswordModel = new ForgotPasswordModel
                {Email = "integration.test@d1.archysoft.com"};

            var requestJson = JsonConvert.SerializeObject(forgotPasswordModel);

            //Action
            //var response = await _sut.Client.PostAsync("/auth/forgot-password", new StringContent(requestJson, Encoding.UTF8, "application/json"));           
            //var resultJson = await response.Content.ReadAsStringAsync();
            //var result = JsonConvert.DeserializeObject<ApiResponse>(resultJson);

            var result = MyActionToAsync(requestJson).Result;
            // Assert
            Assert.Equal(expectedResult, result.Status);
        }


       private async Task<ApiResponse> MyActionToAsync(string requestJson)
       {
           var response = await _sut.Client.PostAsync("/auth/forgot-password", new StringContent(requestJson, Encoding.UTF8, "application/json"));
           var resultJson = await response.Content.ReadAsStringAsync();
           var result= JsonConvert.DeserializeObject<ApiResponse>(resultJson);

           return result;
       }
    }
}
