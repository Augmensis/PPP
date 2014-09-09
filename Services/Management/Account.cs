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
    public class Account
    {
        private const string SQL_AccountSave = "UPDATE citizenDB.Accounts (Salutation,FirstName,LastName,AccountStatus,EmailAddress,PasswordHash,CreatedOn,LastUpdated,PrimaryAddressId,SellingAddressId,BuyingAddressId)VALUES( @Salutation , @FirstName , @LastName , @AccountStatus , @EmailAddress , @PasswordHash , @CreatedOn , @LastUpdated, @PrimaryAddressId , @SellingAddressId , @BuyingAddressId );";
        private const string SQL_NewAccountSave = "INSERT INTO citizenDB.Accounts (Salutation,FirstName,LastName,AccountStatus,EmailAddress,PasswordHash,CreatedOn,LastUpdated)VALUES( @Salutation , @FirstName , @LastName , @AccountStatus , @EmailAddress , @PasswordHash , @CreatedOn , @LastUpdated );";

#region Properties
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

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get;  set; }

        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public static string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Title")]
        [Column("Salutation", TypeName = "VARCHAR")]
        public enSalutaion Salutation { get;  set; }

        [Required]
        [Display(Name = "First Name")]
        [Column("FirstName", TypeName = "VARCHAR")]
        public string FirstName { get;  set; }

        [Required]
        [Display(Name = "Last Name")]
        [Column("LastName", TypeName = "VARCHAR")]
        public string LastName { get;  set; }

        [Display(Name = "Account Status")]
        [Column("AccountStatus", TypeName = "INT")]
        public enAccountSatus AccountStatus { get;  set; }

        public string CreatedOn { get;  set; }

        [ForeignKey("PrimaryAddressId")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PrimaryAddressId { get;  set; }
        [ForeignKey("SellingAddressId")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SellingAddressId { get;  set; }
        [ForeignKey("BuyingAddressId")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BuyingAddressId { get;  set; }
        public Address PrimaryAddress { get;  set; }
        public Address SellingAddress { get;  set; }
        public Address BuyingAddress { get;  set; }
        public List<Address> ObsoleteAddresses { get;  set; }

#endregion Properties

        public Account()
        {
            Id = 0;
            EmailAddress = "";
            Password = "";
            ConfirmPassword = "";
            CreatedOn  = "";
            Salutation = enSalutaion.Mr;
            FirstName  = "";
            LastName = "";
            AccountStatus = enAccountSatus.Seller;
            PrimaryAddress = null;
            SellingAddress = null;
            BuyingAddress = null;
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
            PrimaryAddress = account.PrimaryAddress;
            SellingAddress = account.SellingAddress;
            BuyingAddress = account.BuyingAddress;
        }

        public static Account AddNewAccount(Account acc)
        {
            try
            {
                //@Salutation , @FirstName , @LastName , @AccountStatus , @EmailAddress , @PasswordHash , @CreatedOn , @LastUpdated
                Connection.ExcecuteMySql(SQL_NewAccountSave, new object[] {acc.Salutation, acc.FirstName, acc.LastName, acc.AccountStatus, acc.EmailAddress, acc.Password, acc.CreatedOn, DateTime.Now.ToString()});
                
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
