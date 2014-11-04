using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Management;

namespace PPPConsole
{
    public class SeedData
    {
        public bool UploadSeedDataToServer()
        {
            try
            {
                var seedProducts = new List<Product>
                {
                    new Product() { Name = "Example 1", Description = "Example 1", Processes = null, CreationDate = DateTime.UtcNow, LastUpdated = DateTime.UtcNow },
                    new Product() { Name = "Example 2", Description = "Example 2", Processes = null, CreationDate = DateTime.UtcNow, LastUpdated = DateTime.UtcNow },
                };

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
