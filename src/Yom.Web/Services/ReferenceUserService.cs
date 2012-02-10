using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Yom.Web.Models.User;
using Yom.Lib.Data.EF;

namespace Yom.Web.Services
{
    public class ReferenceUserService :BaseService, IReferenceUserService
    {
        private IUserService UserService;

        public ReferenceUserService(IUserService userService)
        {
            UserService = userService;
        }


        #region IReferenceUserService Members

        public OperationResult Create(string mail, string lastName, string firstName)
        {
            try
            {
                using (YomContainer container = GetYomContainer())
                {
                    var referenceUser = container.ReferenceUsers.Add(new ReferenceUser()
                    {
                        Firstname = firstName,
                        Lastname = lastName,
                        Mail = mail,
                        UserId = UserService.UserGetCurrent().Id,

                    });
                    
                    container.SaveChanges();
                }

                return OperationResult.Success;
            }
            catch (Exception e)
            {
                return OperationResult.Error(e.Message);
            }

        }

        public IEnumerable<ReferenceUser> Get()
        {
            long userId = UserService.UserGetCurrent().Id;

            using (YomContainer container = GetYomContainer())
            {
                return (from i in container.ReferenceUsers
                        where i.UserId == userId
                        select i).ToArray();
            }

        }

        #endregion
    }
}