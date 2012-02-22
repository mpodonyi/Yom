using System;

namespace Yom.Lib.Models.User
{
    public class UserViewModel
    {
        

        public long Id { get; set; }

        public Guid ProviderUserIdId { get; set; }

        public string Mail { get; set; }

        public string Username { get; set; }

        //public string FirstName { get; set; }

        //public string LastName { get; set; }

    }
}