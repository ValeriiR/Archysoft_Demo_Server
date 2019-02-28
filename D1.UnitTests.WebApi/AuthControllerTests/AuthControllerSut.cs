using D1.Model.Services.Abstract;
using Moq;
using WebApi.Controllers;

namespace D1.UnitTests.WebApi.AuthControllerTests
{
    public class AuthControllerSut
    {
        public AuthController Instance { get; set; }

        public Mock<IAuthService> AuthService { get; set; }

        public AuthControllerSut()
        {
            AuthService = new Mock<IAuthService>();
            Instance = new AuthController(AuthService.Object);
        }
    }
}
