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
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
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


        public static List<Product> FetchAllProducts(bool withProcesses = false)
        {
            var dt = Connection.GetMySqlTable("Select top 1 * from Products where IsDeleted <> true ;", new object[] {});
            var productList = new List<Product>();

            foreach (var product in dt.AsEnumerable())
            {
                var prod = new Product
                {
                    Id = Convert.ToInt32(product["id"].ToString()),
                    Name = product["Name"].ToString(),
                    Description = product["Description"].ToString(),
                    Price = Convert.ToDouble(product["Name"].ToString()),
                    CreationDate = product["CreationDate"].ToString(),
                    LastUpdated = product["LastUpdated"].ToString()
                };
                if (withProcesses) FetchProcesses(prod.Id);
                productList.Add(prod);
            }


            //prod.Processes = Process(Convert.ToInt32(dt.Columns["Id"].ToString()));   // Add Method to Content

            return productList;
        }

        public static Product FetchProduct(string productName)
        {
            var dt = Connection.GetMySqlTable("Select top 1 * from Products where Name = @productName ;", new object[]{ productName });
            var prod = new Product
            {
                Id = Convert.ToInt32(dt.Columns["Id"].ToString()), 
                Name = dt.Columns["Name"].ToString(),
                Description = dt.Columns["Description"].ToString(),
                Price = Convert.ToDouble(dt.Columns["Name"].ToString()),
                CreationDate = dt.Columns["CreationDate"].ToString(), 
                LastUpdated = dt.Columns["LastUpdated"].ToString()
            };

            //prod.Processes = Process(Convert.ToInt32(dt.Columns["Id"].ToString()));   // Add Method to Content

            return prod;
        }

        public static Product FetchProduct(int productId)
        {
            var dt = Connection.GetMySqlTable("Select top 1 * from Products where Id = @productId ;", new object[] { productId });
            var prod = new Product
            {
                Id = Convert.ToInt32(dt.Columns["Id"].ToString()),
                Name = dt.Columns["Name"].ToString(),
                Description = dt.Columns["Description"].ToString(),
                Price = Convert.ToDouble(dt.Columns["Name"].ToString()), 
                CreationDate = dt.Columns["CreationDate"].ToString(),
                LastUpdated = dt.Columns["LastUpdated"].ToString()
            };

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
