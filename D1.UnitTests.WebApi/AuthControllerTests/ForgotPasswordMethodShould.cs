using D1.Model.Authentification;
using Xunit;


namespace D1.UnitTests.WebApi.AuthControllerTests
{
    public class ForgotPasswordMethodShould
    {
        public AuthControllerSut Sut { get; set; }

        public ForgotPasswordMethodShould()
        {
            Sut=new AuthControllerSut();
        }

        [Fact]
        public void ReturnStatusMinusOneWhenEmailModelIsNull()
        {
            //Arrange
            int expectedResult = 1;

            //Action
            var actualResult = Sut.Instance.ForgotPassword(null);

            //Assert
            Assert.Equal(expectedResult,actualResult.Status);
        }

        [Fact]
        public void ReturnStatusMinusOneWhenEmailmodelIsNotValid()
        {
            //Arrange
            int expectedResult = 1;
            ForgotPasswordModel notValidEmailModel = new ForgotPasswordModel {Email = "email"};

            //Action
            var actualresult = Sut.Instance.ForgotPassword(notValidEmailModel);

            //Assert
            Assert.Equal(expectedResult,actualresult.Status);
        }
    }
}
