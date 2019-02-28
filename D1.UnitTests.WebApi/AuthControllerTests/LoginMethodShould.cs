using System;
using D1.Model;
using D1.Model.Authentification;
using Moq;
using WebApi.Model;
using Xunit;

namespace D1.UnitTests.WebApi.AuthControllerTests
{
    public class LoginMethodShould
    {
        public AuthControllerSut AuthControllerSut { get; set; }

        public LoginMethodShould()
        {
            AuthControllerSut = new AuthControllerSut();
        }


        [Fact]
        public void ReturnStatusOneLoginModelIsNull()
        {
            //Arrange
            var expectedResult = new ApiResponse<TokenModel>
            {
                Status = 1,
                Description = "Success",
                Timestamp = 1234567,
                Model = null
            };

            //Action
            var actualResult = AuthControllerSut.Instance.Login(null);

            //Assert
            Assert.Equal(expectedResult.Status, actualResult.Status);
        }

        [Fact]
        public void ReturnDescriptionSuccessWhenLoginModelIsNull()
        {
            //Arrange
            var expectedResult = new ApiResponse<TokenModel>
            {
                Status = 1,
                Description = "Success",
                Timestamp = 1234567,
                Model = null
            };

            //Action
            var actualResult = AuthControllerSut.Instance.Login(null);

            //Assert
            Assert.IsType<ApiResponse<TokenModel>>(actualResult);
            Assert.Equal(expectedResult.Description, actualResult.Description);

        }

        [Fact]
        public void ReturnModelNullWhenLoginModelIsNull()
        {
            //Arrange
            //Action
            var actualResult = AuthControllerSut.Instance.Login(null);
            //Assert
            Assert.IsType<ApiResponse<TokenModel>>(actualResult);
            Assert.Null(actualResult.Model);
        }

        [Fact]
        public void ReturnPublicAccessToken()
        {
            //Arrange
            var loginModel = new LoginModel
            {
                Login = "test@email.com",
                Password = "1234qwer"
            };

            var expectedResult = new ApiResponse<TokenModel>
            {
                Model = new TokenModel { AccessToken = "1234567890", ExpiresIn = DateTime.UtcNow.AddDays(1) },
                Timestamp = 123456789,
                Description = "Success",
                Status = 1
            };

            AuthControllerSut.AuthService.Setup(x => x.Login(It.IsAny<LoginModel>())).Returns(new TokenModel
            { AccessToken = "1234567890", ExpiresIn = DateTime.UtcNow.AddDays(1) });
            //Action
            var actualresult = AuthControllerSut.Instance.Login(loginModel);
            //Assert
            Assert.Equal(expectedResult.Model.AccessToken, actualresult.Model.AccessToken);
        }


        [Fact]
        public void ReturnValidExpirationDate()
        {
            //Arrange
            var loginModel = new LoginModel
            {
                Login = "test@email.com",
                Password = "1234qwer"
            };

            var expectedResult = new ApiResponse<TokenModel>
            {
                Model = new TokenModel { AccessToken = "1234567890", ExpiresIn = DateTime.UtcNow.AddDays(1) },
                Timestamp = 123456789,
                Description = "Success",
                Status = 1
            };

            AuthControllerSut.AuthService.Setup(x => x.Login(It.IsAny<LoginModel>())).Returns(new TokenModel
                {AccessToken = "1234567890", ExpiresIn = DateTime.UtcNow.AddDays(1)});

            //Action
            var actualResult = AuthControllerSut.Instance.Login(loginModel);

            //Assert
            Assert.Equal(expectedResult.Model.ExpiresIn.Date,actualResult.Model.ExpiresIn.Date);
        }
    }
}