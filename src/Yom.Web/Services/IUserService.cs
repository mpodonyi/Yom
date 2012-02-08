using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Yom.Web.Models.User;

namespace Yom.Web.Services
{
    public interface IUserService
    {
        MembershipCreateStatus UserCreate(string userName, string email);

        UserViewModel UserGet(string username);

        UserViewModel UserGetCurrent();



    }
}
