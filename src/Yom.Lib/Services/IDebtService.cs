using Yom.Lib.Data;

namespace Yom.Lib.Services
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
