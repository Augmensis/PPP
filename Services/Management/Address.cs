using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Services.Data;

namespace Services.Management
{
    public class Address
    {
        private const string SQL_AddressSave = "INSERT INTO citizenDB.Addresses (AddressLine1, AddressLine2, AddressLine3, City, County, Postcode, AccountId, AddressType) values ( @AddressLine1, @AddressLine2 , @AddressLine3 , @City , @County , @Postcode , @AccountId , @AddressType )";
        private const string SQL_AddressFetch = "Select * from citizenDB.Addresses where AccountId = @accountId ;";

        public enum enAddressType
        {
            primary,
            buying,
            selling,
            obsolete
        }

        public int Id { get; protected set; }
        public string AddressLine1 { get; protected set; }
        public string AddressLine2 { get; protected set; }
        public string AddressLine3 { get; protected set; }
        public string City { get; protected set; }
        public string County { get; protected set; }
        public string Postcode { get; protected set; }
        public int AccountId { get; protected set; }
        public enAddressType AddressType { get; protected set; }

        public Address()
        {
            Id = 0;
            AddressLine1 = "";
            AddressLine2 = "";
            AddressLine3 = "";
            City = "";
            County = "";
            Postcode = "";
            AccountId = 0;
            AddressType = enAddressType.obsolete;
        }

        public static void AddNewAddresses(Account acc, int id)
        {
            try
            {
                //@AddressLine1, @AddressLine2 , @AddressLine3 , @City , @County , @Postcode , @AccountId , @AddressType
                if (acc.PrimaryAddress != null)
                {
                    Connection.ExcecuteMySql(SQL_AddressSave,new object[]{acc.PrimaryAddress.AddressLine1, acc.PrimaryAddress.AddressLine2,acc.PrimaryAddress.AddressLine3, acc.PrimaryAddress.City, acc.PrimaryAddress.County,acc.PrimaryAddress.Postcode, id, enAddressType.primary});
                }
                if (acc.BuyingAddress != null)
                {
                    Connection.ExcecuteMySql(SQL_AddressSave,new object[] {acc.BuyingAddress.AddressLine1, acc.BuyingAddress.AddressLine2, acc.BuyingAddress.AddressLine3,acc.BuyingAddress.City, acc.BuyingAddress.County, acc.BuyingAddress.Postcode, id,enAddressType.buying});
                }
                if (acc.SellingAddress != null)
                {
                    Connection.ExcecuteMySql(SQL_AddressSave, new object[] {acc.SellingAddress.AddressLine1, acc.SellingAddress.AddressLine2,acc.SellingAddress.AddressLine3, acc.SellingAddress.City, acc.SellingAddress.County,acc.SellingAddress.Postcode, id, enAddressType.selling});
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error creating new Address(es): {0}", ex.Message));
            }
        }

        public static List<Address> Fetch(int accountId)
        {
            var dt = Connection.GetMySqlTable(SQL_AddressFetch, new object[] {accountId});
            var addressList = new List<Address>();

            foreach (DataRow addressRow in dt.AsEnumerable())
            {
                var newAddress = new Address();
                newAddress.Id = (int) addressRow["Id"];
                newAddress.AddressLine1 = (string)addressRow["AddressLine1"];
                newAddress.AddressLine2 = (string)addressRow["AddressLine2"];
                newAddress.AddressLine3 = (string)addressRow["AddressLine3"];
                newAddress.City = (string)addressRow["City"];
                newAddress.County = (string)addressRow["County"];
                newAddress.Postcode = (string)addressRow["Postcode"];
                newAddress.AccountId = (int)addressRow["AccountId"];
                newAddress.AddressType = (enAddressType)addressRow["AddressType"];            
                addressList.Add(newAddress);
            }
            dt.Dispose();
            return addressList;
        }

    }
}