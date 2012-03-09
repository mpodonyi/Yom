using System;

namespace Yom.Lib.Models.User
{
    public class UserDebtViewModel:UserViewModel
    {
        public decimal Amount { get; set; }

        public decimal RestAmount { get; set; }
    }
}