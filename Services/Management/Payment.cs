using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Management
{

    public enum enPaymentMethod
    {
        Other = 0,
        Paypal = 1
    }

    public enum enPaymentStatus
    {
        UnPaid = 0,
        Pending = 1,
        Paid = 2,
        Refunded = 3,
        Other = 4
    }


    public class Payment
    {
        public int Id { get; protected set; }
        public int AccountId { get; protected set; }
        public List<int> ProductIds { get; protected set; }
        public enPaymentMethod PaymentMethod { get; protected set; }
        public double Gross { get; protected set; }
        public double Net { get; protected set; }
        public double Vat { get; protected set; }
        public string TransactionCode { get; protected set; }
        public string VoucherCode { get; protected set; }
        public enPaymentStatus PaymentStatus { get; protected set; }

        public DateTime CreatedOn { get; protected set; }
        public DateTime DateCompleted { get; protected set; }
        public DateTime LastUpdated { get; protected set; }
        public bool IsDeleted { get; protected set; }

        public Payment()
        {
            AccountId = 0;
            ProductIds = new List<int>();
            PaymentMethod = enPaymentMethod.Other;
            Gross = 0.0;
            Net = 0.0;
            Vat = 0.0;
            TransactionCode = "";
            VoucherCode = "";
            PaymentStatus = enPaymentStatus.Other;
            CreatedOn = DateTime.UtcNow;
            DateCompleted = DateTime.UtcNow;
            LastUpdated = DateTime.UtcNow;
            IsDeleted = false;
        }

    }


}
