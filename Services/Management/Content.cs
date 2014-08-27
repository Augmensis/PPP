using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Services.Management
{
    public class Content
    {
        public class Overview
        {
            public string Title { get; set; }
            public string Summary { get; set; }
            public DateTime LastUpdated { get; set; }
            public Dictionary<int, string> ProcessDictionary { get; set; }
            public List<String> Notes { get; set; }

            public Overview()
            {
                Title = "";
                Summary = "";
                LastUpdated = new DateTime(2000, 1, 1);
                ProcessDictionary = new Dictionary<int, string>();
                Notes = new List<string>();
            }

            public Overview GetOverviewContent(string selector)
            {
                switch (selector)
                {
                    case "buy":
                        Title = "Buying process";
                        Summary = "Clever shizz here";
                        LastUpdated = DateTime.Now;
                        ProcessDictionary = new Dictionary<int, string> {{1, "Step 1"}, {2, "Step 2"}, {3, "Step 3"}};
                        Notes = new List<string> {{"Here be a note"}, "and another one you scurvey dog!"};
                        break;

                    case "sell":
                        Title = "Buying process";
                        Summary = "Clever shizz here";
                        LastUpdated = DateTime.Now;
                        ProcessDictionary = new Dictionary<int, string> { { 1, "Step 1" }, { 2, "Step 2" }, { 3, "Step 3" } };
                        Notes = new List<string> { { "Here be a note" }, "and another one you scurvey dog!" };
                        break;

                    case "chain":
                        Title = "Buying process";
                        Summary = "Clever shizz here";
                        LastUpdated = DateTime.Now;
                        ProcessDictionary = new Dictionary<int, string> { { 1, "Step 1" }, { 2, "Step 2" }, { 3, "Step 3" } };
                        Notes = new List<string> { { "Here be a note" }, "and another one you scurvey dog!" };
                        break;

                    default:
                        Title = "";
                        Summary = "";
                        LastUpdated = new DateTime(2000, 1, 1);
                        ProcessDictionary = new Dictionary<int, string>();
                        Notes = new List<string>();
                        break;
                }

                return this;
            }
        }
    }
}
