using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Yom.Web.Models.User;
using Yom.Lib.Data.EF;

namespace Yom.Web.Services
{
    public class UserService :BaseService, IUserService
    {
        #region IUserService Members

        public System.Web.Security.MembershipCreateStatus UserCreate(string userName, string email)
        {
            Guid providerUserKey = Guid.NewGuid();
            User user;

            using (YomContainer container = GetYomContainer())
            {
                user = container.Users.Add(new User() { ProviderUserKey = providerUserKey });

                container.SaveChanges();
            }


            System.Web.Security.MembershipCreateStatus membershipCreateStatus;
            Membership.CreateUser(userName, "123456abc*~", email, "yyy", "uuu", true, providerUserKey, out membershipCreateStatus);

            //if (membershipCreateStatus == System.Web.Security.MembershipCreateStatus.Success)
            //{
            //    using (var db = GetYomContainer())
            //    {
            //        db.Users.Add(new User { Mail = email, ProviderUserKey = Guid.NewGuid() });

            //        db.SaveChanges(); 
            //    }

            //}


            return membershipCreateStatus;
        }

        private User GetOrCreate(Guid providerUserKey)
        {
            using (YomContainer container = GetYomContainer())
            {
                User currentYomUser = container.Users.SingleOrDefault(user => user.ProviderUserKey == providerUserKey);

                if (currentYomUser != null)
                    return currentYomUser;

                currentYomUser = container.Users.Add(new User { ProviderUserKey = providerUserKey });

                container.SaveChanges();

                return currentYomUser;
            }
        }

        public UserViewModel UserGet(string username)
        {
            var currentMembershipUser = Membership.GetUser(username);

            if (currentMembershipUser != null)
            {
                var currentYomUser = GetOrCreate((Guid)currentMembershipUser.ProviderUserKey);

                return new UserViewModel
                {
                    Id = currentYomUser.Id,
                    Mail = currentMembershipUser.Email,
                    ProviderUserIdId = (Guid)currentMembershipUser.ProviderUserKey,
                    Username = currentMembershipUser.UserName,
                };
            }

            return null;
        }

        public UserViewModel UserGetCurrent()
        {
            var currentMembershipUser = Membership.GetUser();

            if (currentMembershipUser != null)
            {
                var currentYomUser = GetOrCreate((Guid)currentMembershipUser.ProviderUserKey);

                return new UserViewModel
                {
                    Id = currentYomUser.Id,
                    Mail = currentMembershipUser.Email,
                    ProviderUserIdId = (Guid)currentMembershipUser.ProviderUserKey,
                    Username = currentMembershipUser.UserName,
                };
            }

            return null;
        }

        #endregion
    }
}