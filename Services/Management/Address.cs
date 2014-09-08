using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Services.Data;

namespace Services.Management
{
    public class Address
    {
        private const string SQL_AddressSave = "INSERT INTO citizenDB.Addresses (AddressType, AddressLine1, AddressLine2, AddressLine3 ,City ,County ,PostCode, AccountId)VALUE( @AddressType , @AddressLine1 , @AddressLine2 , @AddressLine3 , @City , @County , @PostCode , @AccountId );";


        public enum enAddressType
        {
            primary,
            selling,
            buying,
            obsolete
        }

        public enAddressType AddressType { get; protected set; }
        public int Id { get; protected set; }
        public string AddressLine1 { get; protected set; }
        public string AddressLine2 { get; protected set; }
        public string AddressLine3 { get; protected set; }
        public string City { get; protected set; }
        public string County { get; protected set; }
        public string AddressPostcode { get; protected set; }

        public Address()
        {
            Id = 0;
            AddressLine1 = "";
            AddressLine2 = "";
            AddressLine3 = "";
            City = "";
            County = "";
            AddressPostcode = "";
        }

        public static void AddNewAddresses(Account acc, int id = 0)
        {
            //@AddressLine1 , @AddressLine2 , @AddressLine3 , @City , @County , @PostCode , @AccountId
            if(acc.PrimaryAddress != null)
            {
                Connection.ExcecuteMySql(SQL_AddressSave, new object[] { acc.PrimaryAddress.AddressLine1, acc.PrimaryAddress.AddressLine2, acc.PrimaryAddress.AddressLine3, acc.PrimaryAddress.City, acc.PrimaryAddress.County, acc.PrimaryAddress.AddressPostcode, id });
            }
            if (acc.SellingAddress != null)
            {
                Connection.ExcecuteMySql(SQL_AddressSave, new object[] { acc.SellingAddress.AddressLine1, acc.SellingAddress.AddressLine2, acc.SellingAddress.AddressLine3, acc.SellingAddress.City, acc.SellingAddress.County, acc.SellingAddress.AddressPostcode, id });
            }
            if (acc.BuyingAddress != null)
            {
                Connection.ExcecuteMySql(SQL_AddressSave, new object[] { acc.BuyingAddress.AddressLine1, acc.BuyingAddress.AddressLine2, acc.BuyingAddress.AddressLine3, acc.BuyingAddress.City, acc.BuyingAddress.County, acc.BuyingAddress.AddressPostcode, id });
            }
        }

        public static List<Address> Fetch(int id)
        {
            var addresses = Connection.GetMySqlTable("Select * from citizenDB.Addresses where AccountId = @accId ;", new object[] { id });
            var addressList = new List<Address>();
            foreach (DataRow address in addresses.AsEnumerable())
            {
                var add = new Address();
                add.Id = (int) address["id"];
                add.AddressLine1 = (string) address["AddressLine1"];
                add.AddressLine2 = (string)address["AddressLine2"];
                add.AddressLine3 = (string) address["AddressLine3"];
                add.City = (string) address["City"];
                add.County = (string) address["County"];
                add.AddressPostcode = (string) address["Postcode"];
                add.AddressType = (enAddressType) address["AddressType"];
                addressList.Add(add);
            }
            return addressList;
        }

    }
}
