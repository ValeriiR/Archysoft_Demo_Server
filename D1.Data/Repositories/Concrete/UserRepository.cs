using D1.Data.Entities;
using D1.Data.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;

namespace D1.Data.Repositories.Concrete
{
    public class UserRepository : Repository<User>, IUserRepository
    {
       
        private readonly UserManager<User> _userManager;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserRepository(DataContext dataContext, UserManager<User> userManager, IPasswordHasher<User> passwordHasher) : base(dataContext)
        {
          
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }

        public User GetUser(string email, string password)
        {
            var user = _userManager.FindByEmailAsync(email).Result;
            if (user != null)
            {
                if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) ==
                    PasswordVerificationResult.Success)
                {
                    return user;
                }
            }

            return null;
        }



        public User GetUser(string email)
        {
            var user = _userManager.FindByEmailAsync(email).Result;
            if (user != null)
            {
                return user;
            }
          
            return null;     
        }
    


        public string GeneratePasswordResetToken(User user)
        {            
           return _userManager.GeneratePasswordResetTokenAsync(user).Result;
        }

       
        public IdentityResult ResetPassword(User user, string password, string token)
        {
            IdentityResult identityResult = _userManager.ResetPasswordAsync(user, token, password).Result;
            if (identityResult.Succeeded)
            {
                SaveChanges();                
            }

            return identityResult;

        }
    }
}
