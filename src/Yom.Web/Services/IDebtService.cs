using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Yom.Web.Models.User;
using Yom.Lib.Data.EF;
using Yom.Web.Data;

namespace Yom.Web.Services
{
    public enum DebtType
    {
        IOweYou = ItemType.IOweYou,
        YouOweMe = ItemType.YouOweMe,
    }


    public interface IDebtService
    {
        OperationResult Create(DebtType debtType, long referenceUserId, decimal amount, string description);


    }
}
