using System;

namespace Payments.API.Models
{
    public class PaymentInfoModel
    {
        public PaymentInfoModel()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public bool CreditCard { get; set; }
        public bool IsConfirmed { get; set; }
        public string Description { get; set; }
    }
}
