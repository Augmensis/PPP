using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Data;

namespace Services.Management
{
    class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description  { get; set; }

        [Required]
        public double Price { get; set; }

        public string CreationDate { get; set; }
        public string LastUpdated { get; set; }
        public List<Content> Process  { get; set; }


        public Product()
        {
            Name = "";
            Description = "";
            Price = 0.0d;
            CreationDate = "";
            LastUpdated = "";
            Process = new List<Content>();
        }

        public static Product FetchProduct(string productName)
        {
            var dt = Connection.GetMySqlTable("Select top 1 * from Products where Name = @productName ;", new object[]{ productName });
            var prod = new Product();

            prod.Id = Convert.ToInt32(dt.Columns["Id"].ToString());
            prod.Name = dt.Columns["Name"].ToString();
            prod.Description = dt.Columns["Description"].ToString();
            prod.Price = Convert.ToDouble(dt.Columns["Name"].ToString());
            prod.CreationDate = dt.Columns["CreationDate"].ToString();
            prod.LastUpdated = dt.Columns["LastUpdated"].ToString();
            prod.Process = Content.FetchProductProcess(Convert.ToInt32(dt.Columns["Id"].ToString()));   // Add Method to Content

            return prod;
        }

        //public static List<Content> FetchOverview(int productId)
        //{
        //    var dt = Connection.GetMySqlTable("Select * from Content where ProductId = @productId and ContentType = 0;", new object[] {productId});

        //    var overviewList = new List<Content>();
        //    foreach (DataRow overviewItem in dt.AsEnumerable())
        //    {
        //        var overview = new Overview();
        //        overview.ControllerName = (string)overviewItem["ControllerName"];
        //    }


        //}

        public static List<Content> FetchProcesses(int productId)
        {
            return new List<Content>();
        }

        public static void Create(Product product)
        {
            
        }

        public static Content AddOverviewStep(Product product)
        {
            return new Content();
        }

        public static Content AddProcessStep(Product product)
        {
            return new Content();
        }

        public static void Update(Product product)
        {
            
        }

    }
}
