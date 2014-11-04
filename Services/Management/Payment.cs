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
        public int Id { get;  set; }
        public int AccountId { get;  set; }
        public List<int> ProductIds { get;  set; }
        public enPaymentMethod PaymentMethod { get;  set; }
        public double Gross { get;  set; }
        public double Net { get;  set; }
        public double Vat { get;  set; }
        public string TransactionCode { get;  set; }
        public string VoucherCode { get;  set; }
        public enPaymentStatus PaymentStatus { get;  set; }

        public DateTime CreatedOn { get;  set; }
        public DateTime DateCompleted { get;  set; }
        public DateTime LastUpdated { get;  set; }
        public bool IsDeleted { get;  set; }

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
