using D1.Model.Authentification;
using D1.Model.Exceptions;
using Xunit;

namespace D1.UnitTests.WebApi.AuthServiceTests
{
    public class ForgotPasswordMethodShould
    {
        public AuthServiceSut Sut { get; set; }

        public ForgotPasswordMethodShould()
        {
            Sut=new AuthServiceSut();
        }

        [Fact]
        public void ReturnExceptionIfEmailIsNotCorrect()
        {
            //Arrange          
            ForgotPasswordModel email=new ForgotPasswordModel{Email = "email@gmail.com" };
                         
            //Action          
            // Assert
            Assert.Throws<BusinessException>(()=>Sut.Instance.ForgotPassword(email));
        }

        [Fact]
        public void RunOkIEmailIfIsCorrect()
        {
            //Arrange         
            ForgotPasswordModel email = new ForgotPasswordModel { Email = "admin@email.com" };

            //Action          
            // Assert
            Assert.Throws<BusinessException>(() => Sut.Instance.ForgotPassword(email));
        }
    }
}
