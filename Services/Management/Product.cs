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
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; protected set; }

        [Required]
        public string Name { get; protected set; }

        [Required]
        public string Description { get; protected set; }

        [Required]
        public double Price { get; protected set; }
        public double EstimatedTotalLegalCost { get; protected set; }

        public string CreationDate { get; protected set; }
        public string LastUpdated { get; protected set; }
        public List<Process> Processes { get; protected set; }
        public bool IsDeleted { get; protected set; }

        public Product()
        {
            Name = "";
            Description = "";
            Price = 0.0d;
            CreationDate = "";
            LastUpdated = "";
            Processes = new List<Process>();
            IsDeleted = false;
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
            //prod.Processes = Process(Convert.ToInt32(dt.Columns["Id"].ToString()));   // Add Method to Content

            return prod;
        }

        public static Product FetchProduct(int productId)
        {
            var dt = Connection.GetMySqlTable("Select top 1 * from Products where Id = @productId ;", new object[] { productId });
            var prod = new Product();

            prod.Id = Convert.ToInt32(dt.Columns["Id"].ToString());
            prod.Name = dt.Columns["Name"].ToString();
            prod.Description = dt.Columns["Description"].ToString();
            prod.Price = Convert.ToDouble(dt.Columns["Name"].ToString());
            prod.CreationDate = dt.Columns["CreationDate"].ToString();
            prod.LastUpdated = dt.Columns["LastUpdated"].ToString();
            //prod.Overview = Content.FetchProductOverview(Convert.ToInt32(dt.Columns["Id"].ToString()));   // Add Method to Content
            //prod.Processes = Content.FetchProductProcess(Convert.ToInt32(dt.Columns["Id"].ToString()));   // Add Method to Content

            return prod;
        }

        public static List<Process> FetchProcesses(int productId)
        {
            return new List<Process>();
        }

        public static void Create(Product product)
        {
            
        }

        public static Process AddProcessStep(Product product)
        {
            return new Process();
        }

        public static void Update(Product product)
        {
            
        }

    }
}
