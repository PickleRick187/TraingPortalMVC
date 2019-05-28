using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingPortal.BLL.Interfaces;
using TrainingPortal.DAL;

namespace TrainingPortal.BLL.Repositories
{
    class PaymentRepository : Repository<PaymentType>, IPaymentRepository
    {
        public PaymentRepository(TrainingPortalEntities context) : base(context)
        {
            
        }

        public PaymentType PurchaseCourse(PaymentType paymentType)
        {
            var queryPay = TrainingPortalEntities.PaymentTypes.Add(paymentType);

            return queryPay;
        }


        public TrainingPortalEntities TrainingPortalEntities
        {
            get { return _context as TrainingPortalEntities; }
        }
    }
}
