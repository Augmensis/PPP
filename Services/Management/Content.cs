using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using Services.Data;


namespace Services.Management
{
    public class Content
    {
        protected const string SQL_ContentInsert = "INSERT INTO citizenDB.Contents (Type, Title, Summary, ControllerName ,LastUpdated ,CreationDate ,ViewName, Position ) VALUES ( @Type , @Title , @Summary , @ControllerName , @LastUpdated , @CreationDate , @ViewName , @Position );";
        protected const string SQL_ConnectGetAll = "Select * from citizenDB.Content;";
        protected const string SQL_ConnectGetSellingOverview = "Select Position, title, lastupdated from citizenDB.Contents where ControllerName = 'selling' and type = 2 order by Position asc;";
        protected const string SQL_ConnectGetBuyingOverview = "Select Position, title, lastupdated from citizenDB.Contents where ControllerName = 'buying' and type = 1 order by Position asc;";
        protected const string SQL_ConnectGetChainOverview = "Select Position, title, lastupdated from citizenDB.Contents where ControllerName = 'chain' and type = 3 order by Position asc;";
        protected const string SQL_ConnectGetSellingNotes = "Select Position, title, lastupdated from citizenDB.Contents where ControllerName = 'selling' and type = 4 order by Position asc;";
        protected const string SQL_ConnectGetBuyingNotes = "Select Position, title, lastupdated from citizenDB.Contents where ControllerName = 'buying' and type = 4 order by Position asc;";
        protected const string SQL_ConnectGetChainNotes = "Select Position, title, lastupdated from citizenDB.Contents where ControllerName = 'chain' and type = 4 order by Position asc;";


        public enum enProductType{
            Nothing = 0,
            Buying = 1,
            Selling = 2,
            Chain = 3,
            Notes = 4
        }

        public enum enContentType
        {
            Overview = 0,
            Process = 1,
            Note = 2,
            Tip = 3
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public DateTime LastUpdated { get; set; }
        public enContentType ContentType { get; set; }
        public enProductType ProductType { get; set; }


#region Admin Inserts

        public static List<Overview> FetchProductOverview(int productId)
        {
            var dt = Connection.GetMySqlTable(SQL_GetProductOverview, new object[] {productId});

            foreach (DataRow ov in dt.AsEnumerable())
            {
                var overveiw = new Overview();
                overveiw.Title = (string) ov["Title"];
                overveiw.Description = (string) ov["Title"];
                overveiw.ProductId = Convert.ToInt32(ov["Title"].ToString());
                overveiw.Position = Convert.ToInt32(ov["Title"].ToString());
                overveiw.LastUpdated = Convert.ToDateTime(ov["Title"].ToString());
                overveiw.ContentType = (enContentType) Convert.ToInt32(ov["Title"].ToString());
                overveiw.ProductType = (enProductType) Convert.ToInt32(ov["Title"].ToString());
            }
        }

        public static List<Content> FetchProductProcess(int productId)
        {

        }

        public static void AddOverviewToSeller()
        {
            var tempDictionary = new Dictionary<int, string>();
            var i = 1;
            tempDictionary.Add(i, "Send Form OC1 (or OC2 if neccesary) for Office Copies.");
            tempDictionary.Add(++i, "Obtain a copy of the Title Plan.");
            tempDictionary.Add(++i, "Obtain EPC (Energy Performance Certificate) for the proerty.");
            tempDictionary.Add(++i, "Obtain copies of any documents referred to in the register.");
            tempDictionary.Add(++i, "Obtain copies of any lease from the Land Registry.");
            tempDictionary.Add(++i, "Send documents to buyer / buyer's solicitor (Draft Contract, Property Information Forms, Office Copies, Fixtures, Fittings & Contents Form).");
            tempDictionary.Add(++i, "Respond to any enquiries from the buyer.");
            tempDictionary.Add(++i, "Make it clear when, where and how you want to get paid.");
            tempDictionary.Add(++i, "If you have broken any Covenants within the last 20 years, buy Covenant Indemnity Insurance for the buyer... at your own expense. Also write the event into the contract.");
            tempDictionary.Add(++i, "When the contract has been returned, check for any changes & when happy, sign and return it to the buyer / buyer's solicitor.");
            tempDictionary.Add(++i, "Contracts are now Exchanged.");
            tempDictionary.Add(++i, "Receive the Draft Transfer from the buyer, checking payment price and spellings of all names and addresses.");
            tempDictionary.Add(++i, "Receive Requisitions on Title from the buyer & answer all questions that are raised.");
            tempDictionary.Add(++i, "Prepare the completion statement.");
            tempDictionary.Add(++i, "Sign Transfer form TR1 (or TP1 or TR2).");
            tempDictionary.Add(++i, "You've just completed the transaction. Well done!");

            foreach (var step in tempDictionary)
            {
                InsertToContentTable(enProductType.Selling, step.Value, "", "Selling", DateTime.UtcNow, "Overview", step.Key);
            }

        }

        protected static void InsertToContentTable(enProductType type, string title, string summary, string controllerName, DateTime date, string viewName, int position)
        {
            try
            {
                Connection.ExcecuteMySql(SQL_ContentInsert, new object[] { type, title, summary, controllerName, date.ToString(), date.ToString(), viewName, position });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }



#endregion

    }
}
