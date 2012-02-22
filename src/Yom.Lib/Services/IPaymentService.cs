using Yom.Lib.Data;

namespace Yom.Lib.Services
{
    public enum PaymentType
    {
        IPayYou = ItemType.IPayYou,
        YouPayMe = ItemType.YouPayMe,
    }


    public interface IPaymentService
    {
        OperationResult Create(PaymentType paymentType, long referenceUserId, decimal amount, string description);


    }
}
