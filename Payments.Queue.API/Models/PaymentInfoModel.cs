using System;

namespace Payments.Queue.API.Models
{
    public class PaymentInfoModel
    {
        public Guid Id { get; set; } 
        public bool CreditCard { get; set; }
        public bool IsConfirmed { get; set; }
        public string Description { get; set; }
    }
}
