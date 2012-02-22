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
    public class PaymentService :BaseService, IPaymentService
    {
        private IUserService UserService;

        public PaymentService(IUserService userService)
        {
            UserService = userService;
        }

        public OperationResult Create(PaymentType paymentType, long referenceUserId, decimal amount, string description)
        {
            try
            {
                using (YomContainer container = GetYomContainer())
                {
                    Item item = container.Items.Add(new Item
                    {
                        Type = (short)paymentType,
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