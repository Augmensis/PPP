using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Services.Management
{
    class Account
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
        }

        public string AccountNumber { get; protected set; }
        public string EmailAddress { get; protected set; }
        public string PasswordHash { get; protected set; }
        public string CreatedOn { get; protected set; }
        public string Salutation { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string AddressLine1 { get; protected set; }
        public string AddressLine2 { get; protected set; }
        public string AddressLine3 { get; protected set; }
        public string AddressLine4 { get; protected set; }
        public string AddressLine5 { get; protected set; }
        public string AddressPostcode { get; protected set; }
        public string AccountStatus { get; protected set; }

    }
}
