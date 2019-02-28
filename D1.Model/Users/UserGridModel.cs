using System;

namespace D1.Model.Users
{
    public class UserGridModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
