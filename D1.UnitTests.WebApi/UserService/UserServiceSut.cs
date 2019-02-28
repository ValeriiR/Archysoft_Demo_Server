using AutoMapper;
using D1.Data.Repositories.Abstract;
using D1.Model.Services.Concrete;
using Moq;

namespace D1.UnitTests.WebApi.UserService
{
    public class UserServiceSut
    {
        public Model.Services.Concrete.UserService Instance { get; set; }

        public  Mock<IUserRepository> UserRepository { get; set; }
        public Mock<IMapper> Mapper { get; set; }


        public UserServiceSut()
        {
            UserRepository=new Mock<IUserRepository>();

            Mapper=new Mock<IMapper>();
            Instance=new Model.Services.Concrete.UserService(UserRepository.Object,Mapper.Object);
        }

    }
}
