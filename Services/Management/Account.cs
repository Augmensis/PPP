using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Services.Management
{
    public class Account
    {
        public enum enSalutaion
        {
            Mr,
            Mrs,
            Miss,
            Ms,
            Dr,
            Sir,
            Lord,
            Other
        }

        public enum enAccountSatus
        {
            Buyer,
            Seller,
            Chain,
            Other
        }

        public string AccountNumber { get; protected set; }
        public string EmailAddress { get; protected set; }
        public string PasswordInput { get; protected set; }
        public string PasswordHash { get; protected set; }
        public string CreatedOn { get; protected set; }
        public enSalutaion Salutation { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public enAccountSatus AccountStatus { get; protected set; }
        public Address CurrentAddress { get; protected set; }
        public Address SellingAddress { get; protected set; }
        public Address BuyingAddress { get; protected set; }


        public Account()
        {
            AccountNumber = "";
            EmailAddress = "";
            PasswordHash = "";
            CreatedOn  = "";
            Salutation = enSalutaion.Mr;
            FirstName  = "";
            LastName = "";
            AccountStatus = enAccountSatus.Seller;
            CurrentAddress = new Address();
            SellingAddress = null;
            BuyingAddress = null;
        }

        public class Address
        {
            public string AddressLine1 { get; protected set; }
            public string AddressLine2 { get; protected set; }
            public string AddressLine3 { get; protected set; }
            public string City { get; protected set; }
            public string County { get; protected set; }
            public string AddressPostcode { get; protected set; }

            public Address()
            {
                AddressLine1 = "";
                AddressLine2 = "";
                AddressLine3 = "";
                City = "";
                County = "";
                AddressPostcode = "";
            }
        }
    }
}
