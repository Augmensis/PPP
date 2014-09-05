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
    public class Account
    {
        private const string SQL_AccountSave = "UPDATE citizenDB.Accounts (Salutation,FirstName,LastName,AccountStatus,EmailAddress,PasswordHash,CreatedOn,LastUpdated,PrimaryAddressId,SellingAddressId,BuyingAddressId)VALUES( @Salutation , @FirstName , @LastName , @AccountStatus , @EmailAddress , @PasswordHash , @CreatedOn , @LastUpdated, @PrimaryAddressId , @SellingAddressId , @BuyingAddressId );";
        private const string SQL_NewAccountSave = "INSERT INTO citizenDB.Accounts (Salutation,FirstName,LastName,AccountStatus,EmailAddress,PasswordHash,CreatedOn,LastUpdated)VALUES( @Salutation , @FirstName , @LastName , @AccountStatus , @EmailAddress , @PasswordHash , @CreatedOn , @LastUpdated );";

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

        public int Id { get; protected set; }
        public string EmailAddress { get; protected set; }
        public string PasswordInput { get; protected set; }
        public string PasswordHash { get; protected set; }
        public string CreatedOn { get; protected set; }
        public enSalutaion Salutation { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public enAccountSatus AccountStatus { get; protected set; }
        public int PrimaryAddressId { get; protected set; }
        public int SellingAddressId { get; protected set; }
        public int BuyingAddressId { get; protected set; }
        public Address PrimaryAddress { get; protected set; }
        public Address SellingAddress { get; protected set; }
        public Address BuyingAddress { get; protected set; }
        public List<Address> ObsoleteAddresses { get; protected set; } 


        public Account()
        {
            Id = 0;
            EmailAddress = "";
            PasswordHash = "";
            CreatedOn  = "";
            Salutation = enSalutaion.Mr;
            FirstName  = "";
            LastName = "";
            AccountStatus = enAccountSatus.Seller;
            PrimaryAddress = null;
            SellingAddress = null;
            BuyingAddress = null;
        }



        public Account AddNewAccount(Account acc)
        {
            try
            {
                //@Salutation , @FirstName , @LastName , @AccountStatus , @EmailAddress , @PasswordHash , @CreatedOn , @LastUpdated
                Connection.ExcecuteMySql(SQL_NewAccountSave, new object[] {acc.Salutation, acc.FirstName, acc.LastName, acc.AccountStatus, acc.EmailAddress, acc.PasswordHash, acc.CreatedOn, DateTime.Now.ToString()});
                
                if (acc.PrimaryAddress != null || acc.SellingAddress != null || acc.BuyingAddress != null)
                {
                    var id = Fetch(acc.EmailAddress).Id;
                    Address.AddNewAddresses(acc, id);
                }
                return acc;
            }

            catch (Exception ex)
            {
                throw new Exception(String.Format("Error with Adding a new Account: {0}", ex.Message));
            }
        }


        public Account Fetch(string email, bool withAddresses = false)
        {
            try
            {
                var dt = Connection.GetMySqlTable("Select * from citizenDB.Accounts where EmailAddress = @email ;", new object[] {email});

                var acc = new Account();
                acc.Id = dt.Rows[0].Field<int>("id");
                acc.FirstName = dt.Rows[0].Field<string>("FirstName");
                acc.LastName = dt.Rows[0].Field<string>("LastName");
                acc.EmailAddress = dt.Rows[0].Field<string>("EmailAddress");
                acc.Salutation = dt.Rows[0].Field<enSalutaion>("Salutation");
                acc.AccountStatus = dt.Rows[0].Field<enAccountSatus>("AccountStatus");
                acc.SellingAddressId =  dt.Rows[0].Field<int>("SellingAddressId");
                acc.BuyingAddressId = dt.Rows[0].Field<int>("BuyingAddressId");
                acc.PrimaryAddressId = dt.Rows[0].Field<int>("PrimaryAddressId");

                if (withAddresses)
                {
                    var addresses = Address.Fetch(acc.Id);
                    foreach (Address address in addresses)
                    {
                        switch (address.AddressType)
                        {
                            case Address.enAddressType.primary:
                                acc.PrimaryAddress = address;
                                break;
                            case Address.enAddressType.buying:
                                acc.BuyingAddress = address;
                                break;
                            case Address.enAddressType.selling:
                                acc.SellingAddress = address;
                                break;
                            case Address.enAddressType.obsolete:
                                acc.ObsoleteAddresses.Add(address);
                                break;
                        }
                    }
                }

                return acc;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error with Fetching a new Account: {0}", ex.Message));
            }
                
        }

        public bool Save(Account acc)
        {
            try
            {
                // Connection.ExcecuteMySql(SQL_AccountSave);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error trying to save Account changes: {0}", ex.Message));
            }
            return false;
        }
    }
}
