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
        private const string SQL_ProductSave = "";
        private const string SQL_ProductUpdate = "";
        
        public int Id { get;  set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
        public double Price { get;  set; }
        public double EstimatedTotalLegalCost { get;  set; }

        public DateTime CreationDate { get;  set; }
        public DateTime LastUpdated { get;  set; }
        public List<Process> Processes { get;  set; }
        public bool IsDeleted { get;  set; }

        public Product()
        {
            Name = "";
            Description = "";
            Price = 0.0d;
            CreationDate = DateTime.UtcNow;
            LastUpdated = DateTime.UtcNow;
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
                    CreationDate = Convert.ToDateTime(product["CreationDate"].ToString()),
                    LastUpdated = Convert.ToDateTime(product["LastUpdated"].ToString())
                };
                if (withProcesses) prod.Processes = Process.FetchAllProcesses(prod.Id);
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
                CreationDate = Convert.ToDateTime(dt.Columns["CreationDate"].ToString()), 
                LastUpdated = Convert.ToDateTime(dt.Columns["LastUpdated"].ToString())
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
                CreationDate = Convert.ToDateTime(dt.Columns["CreationDate"].ToString()),
                LastUpdated = Convert.ToDateTime(dt.Columns["LastUpdated"].ToString())
            };

            return prod;
        }


        public bool Save(Product acc)
        {
            try
            {
                Connection.ExcecuteMySql(SQL_ProductSave);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error trying to save Process changes: {0}", ex.Message));
            }
            return false;
        }

        public static Process AddProcessStep(Product product)
        {
            return new Process();
        }

        public static bool Update(Product product)
        {
            try
            {
                Connection.ExcecuteMySql(SQL_ProductUpdate);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error trying to save Process changes: {0}", ex.Message));
            }
            return false;
        }



    }
}
