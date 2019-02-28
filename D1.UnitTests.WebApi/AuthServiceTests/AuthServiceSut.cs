
using D1.Data.Entities;
using D1.Data.Repositories.Abstract;
using D1.Model.Services.Abstract;
using D1.Model.Services.Concrete;
using Moq;


namespace D1.UnitTests.WebApi.AuthServiceTests
{
    public class AuthServiceSut
    {
        public AuthService Instance { get; set; }

        public Mock<IUserRepository> UserRepository { get; set; }
        public Mock<ISettingsService> SettingsService { get; set; }
        public Mock<IEmailService> EmailService { get; set; }



        public AuthServiceSut()
        {
            UserRepository = new Mock<IUserRepository>();
            UserRepository.Setup(a => a.GetUser("admin@email.com")).Returns(new User());
            SettingsService = new Mock<ISettingsService>();
            EmailService = new Mock<IEmailService>();

            Instance = new AuthService(UserRepository.Object, SettingsService.Object, EmailService.Object);
        }
    }
}
