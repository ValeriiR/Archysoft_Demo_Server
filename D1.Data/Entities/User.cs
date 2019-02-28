using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace D1.Data.Entities
{
    public class User:IdentityUser<Guid>
    {
        public UserProfile Profile { get; set; }
    }
}
