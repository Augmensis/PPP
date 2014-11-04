using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Services.Data;
using System.ComponentModel.DataAnnotations;

namespace Services.Management
{
    public enum enAccountSatus
    {
        NonPaying = 0,
        Paying = 1
    }

    public class Account
    {
        private const string SQL_AccountSave = "UPDATE citizenDB.Accounts (Salutation,FirstName,LastName,AccountStatus,EmailAddress,PasswordHash,CreatedOn,LastUpdated,PrimaryAddressId,SellingAddressId,BuyingAddressId)VALUES( @Salutation , @FirstName , @LastName , @AccountStatus , @EmailAddress , @PasswordHash , @CreatedOn , @LastUpdated, @PrimaryAddressId , @SellingAddressId , @BuyingAddressId );";
        private const string SQL_NewAccountSave = "INSERT INTO citizenDB.Accounts (Salutation,FirstName,LastName,AccountStatus,EmailAddress,PasswordHash,CreatedOn,LastUpdated)VALUES( @Salutation , @FirstName , @LastName , @AccountStatus , @EmailAddress , @PasswordHash , @CreatedOn , @LastUpdated );";

#region Properties

        public int Id { get; protected set; }
        public string EmailAddress { get; protected set; }
        public string Password { get; protected set; }
        public string ConfirmPassword { get; protected set; }

        public string Salutation { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public enAccountSatus AccountStatus { get; protected set; }
        public List<Payment> Payments { get; protected set; }

        public int PrimaryAddressId { get; protected set; }
        public int SellingAddressId { get; protected set; }
        public int BuyingAddressId { get; protected set; }
        public Address PrimaryAddress { get; protected set; }
        public Address SellingAddress { get; protected set; }
        public Address BuyingAddress { get; protected set; }
        public List<Address> ObsoleteAddresses { get; protected set; }

        public DateTime CreatedOn { get; protected set; }
        public DateTime LastUpdated { get; protected set; }
        public bool IsDeleted { get; protected set; }

#endregion Properties

        public Account()
        {
            Id = 0;
            EmailAddress = "";
            Password = "";
            ConfirmPassword = "";
            Salutation = "";
            FirstName  = "";
            LastName = "";
            AccountStatus = enAccountSatus.NonPaying;
            PrimaryAddressId = 0;
            SellingAddressId = 0;
            BuyingAddressId = 0;
            PrimaryAddress = null;
            SellingAddress = null;
            BuyingAddress = null;
            CreatedOn = DateTime.Now;
            LastUpdated = DateTime.Now;
            IsDeleted = false;
        }

        public Account (Account account)
        {
            Id = account.Id;
            EmailAddress = account.EmailAddress;
            Password = account.Password;
            ConfirmPassword = account.ConfirmPassword;
            CreatedOn = account.CreatedOn;
            Salutation = account.Salutation;
            FirstName = account.FirstName;
            LastName = account.LastName;
            AccountStatus = account.AccountStatus;
            PrimaryAddressId = account.PrimaryAddressId;
            SellingAddressId = account.SellingAddressId;
            BuyingAddressId = account.BuyingAddressId;
            PrimaryAddress = account.PrimaryAddress;
            SellingAddress = account.SellingAddress;
            BuyingAddress = account.BuyingAddress;
            CreatedOn = account.CreatedOn;
            LastUpdated = account.LastUpdated;
            IsDeleted = account.IsDeleted;
        }

        public static Account AddNewAccount(Account acc)
        {
            try
            {
                //@Salutation , @FirstName , @LastName , @AccountStatus , @EmailAddress , @PasswordHash , @CreatedOn , @LastUpdated
                Connection.ExcecuteMySql(SQL_NewAccountSave, new object[] {acc.Salutation, acc.FirstName, acc.LastName, acc.AccountStatus, acc.EmailAddress, acc.Password, acc.CreatedOn, DateTime.Now.ToString()});
                
                if (acc.PrimaryAddressId != 0 || acc.SellingAddressId != 0 || acc.BuyingAddressId != 0)
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


        public static Account Fetch(string email, bool withAddresses = false)
        {
            try
            {
                var dt = Connection.GetMySqlTable("Select * from citizenDB.Accounts where EmailAddress = @email ;", new object[] {email});

                var acc = new Account();
                acc.Id = dt.Rows[0].Field<int>("id");
                acc.FirstName = dt.Rows[0].Field<string>("FirstName");
                acc.LastName = dt.Rows[0].Field<string>("LastName");
                acc.EmailAddress = dt.Rows[0].Field<string>("EmailAddress");
                acc.Salutation = dt.Rows[0].Field<string>("Salutation");
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
                            case Address.enAddressType.Primary:
                                acc.PrimaryAddress = address;
                                break;
                            case Address.enAddressType.Buying:
                                acc.BuyingAddress = address;
                                break;
                            case Address.enAddressType.Selling:
                                acc.SellingAddress = address;
                                break;
                            case Address.enAddressType.Obsolete:
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
                Connection.ExcecuteMySql(SQL_AccountSave);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error trying to save Account changes: {0}", ex.Message));
            }
            return false;
        }

        //public bool GoToPub(bool pubExists, bool beerExists)
        //{
        //    while (pubExists || beerExists)
        //    {
        //        Console.WriteLine("Might as well go to the pub");
        //        GoToPub(pubExists, beerExists);
        //    }
        //    return true;
        //}
    }
}
