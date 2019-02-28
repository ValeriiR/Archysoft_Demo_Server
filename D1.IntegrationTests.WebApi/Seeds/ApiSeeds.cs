using D1.Data;
using System;
using D1.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace D1.IntegrationTests.WebApi.Seeds
{
    public static class ApiSeeds
    {
        public static void SeedUsers(this ApiSut sut)
        {
            using (var context = sut.Context)
            {
                if (!context.Users.Any())
                {
                    var store = new UserStore<User, Role, DataContext, Guid>(context);
                    var passwordHasher = new PasswordHasher<User>();
                    var userManager = new UserManager<User>(store, null, passwordHasher, null, null, null, null, null, null);
                    var user = new User
                    {
                        UserName = "IntegrationTest",
                        Email = "integration.test@d1.archysoft.com",
                        EmailConfirmed = true
                    };

                    var result = userManager.CreateAsync(user, "Pa$$w0rd1").Result;
                }
            }
        }
    }
}
