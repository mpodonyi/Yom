using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Yom.Lib.Models.User;
using Yom.Lib.Data.EF;
using Yom.Lib.Services;

namespace Yom.Lib.ServicesImpl
{
    public class ReferenceUserService : BaseService, IReferenceUserService
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






        public IEnumerable<UserDebtViewModel> GetOutstandingDebtUser(PaymentType debtType)
        {
            long userId = UserService.UserGetCurrent().Id;

            using (YomContainer container = GetYomContainer())
            {
                var payitems = from i in container.Items
                               join oi in container.PayItems on i.Id equals oi.Id
                               where i.Type == (short)debtType &&
                               i.UserId == userId
                               group oi by new { i.UserId, oi.OweItemId } into k
                               select new
                               {
                                   OweItemId = k.Key.OweItemId,
                                   UserId = k.Key.UserId,
                                   Amount = k.Sum(a => a.Amount)
                               };

                var oweitems = from i in container.Items
                               join oi in container.OweItems on i.Id equals oi.Id into k
                               where i.Type == (short)DebtType.YouOweMe &&
                                  i.UserId == userId
                               select k;

            //    var ttt = oweitems.Distinct()




            }

        }

    }
}