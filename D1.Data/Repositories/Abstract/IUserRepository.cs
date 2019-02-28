

using D1.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace D1.Data.Repositories.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUser(string login, string password);
        User GetUser(string login);
       // User GetUserById(Guid id);

        IdentityResult ResetPassword(User user, string password,string token);

        string GeneratePasswordResetToken(User user);

      
    }
}
