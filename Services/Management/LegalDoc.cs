using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Management
{
    class LegalDoc
    {
        public int Id { get; set; }
        public string DocumentTitle { get; set; }
        public string Description { get; set; }
        public string GenericCode { get; set; }
        public string OuyezCode { get; set; }
        public double EstimatedPrice { get; set; }

        public LegalDoc()
        {
            DocumentTitle = "";
            Description = "";
            GenericCode = "";
            OuyezCode = "";
            EstimatedPrice = 0.0d;
        }
    }
}
