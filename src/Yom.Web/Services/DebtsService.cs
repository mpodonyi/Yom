using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Yom.Web.Models.User;
using Yom.Lib.Data.EF;

namespace Yom.Web.Services
{
    public class DebtService : IDebtService
    {
        private IUserService UserService;

        public DebtService(IUserService userService)
        {
            UserService = userService;
        }




        public OperationResult Create(DebtType debtType, long referenceUserId, decimal amount, string description)
        {
            try
            {
                using (YomContainer container = new YomContainer())
                {
                    Item item = container.Items.Add(new Item
                    {
                        Type = (short)debtType,
                        UserId = UserService.UserGetCurrent().Id,
                        OweItem = new OweItem
                        {
                            Amount = amount,
                            ReferenceUser = referenceUserId,
                            Description = description,
                        }
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




    }
}