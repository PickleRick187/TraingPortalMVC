using TrainingPortal.DAL;

namespace TrainingPortal.BLL.Interfaces
{
    public interface IPaymentRepository : IRepository<PaymentType>
    {
        PaymentType PurchaseCourse(PaymentType paymentType);
    }
}
