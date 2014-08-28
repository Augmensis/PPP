using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Services.Management
{
    public class Content
    {
        public enum enControllerTypes
        {
            nothing,
            buying,
            selling,
            chain
        }

        public class Overview
        {
            public string Title { get; protected set; }
            public string Summary { get; protected set; }
            public DateTime LastUpdated { get; protected set; }
            public Dictionary<int, string> ProcessDictionary { get; protected set; }
            public List<String> Notes { get; protected set; }
            public string ControllerName { get; protected set; }

            public Overview()
            {
                Title = "";
                Summary = "";
                LastUpdated = new DateTime(2000, 1, 1);
                ProcessDictionary = new Dictionary<int, string>();
                Notes = new List<string>();
                ControllerName = "";
            }

            public Overview GetOverview(string id)
            {
                switch (id)
                {
                    case "buy":
                        Title = "Buying Process";
                        Summary = "Here is a complete outline of the basic buying process, from start to finish, for a registered property in the UK. Whilst it doesn't include all of the technicalities that need to be considered, or help you fill out any forms, it is enough to show you what to expect when buying a house without using a solicitor or conveyancer.";
                        LastUpdated = new DateTime(2014, 08, 28);
                        ProcessDictionary = new Dictionary<int, string> {{1, "Step 1"}, {2, "Step 2"}, {3, "Step 3"}};
                        Notes = new List<string> {"Here be a note", "and another one you scurvey dog!"};
                        ControllerName = "buying";
                        break;

                    case "sell":
                        Title = "Selling Process";
                        Summary = "Here is a complete outline of the basic selling process, from start to finish, for a registered property in the UK. Whilst it doesn't include all of the technicalities that need to be considered, or help you fill out any forms, it is enough to show you what to expect when selling a house without using a solicitor or estate agent.";
                        LastUpdated = new DateTime(2014, 08, 28);
                        ProcessDictionary = new Dictionary<int, string> {{1, "Step 1"}, {2, "Step 2"}, {3, "Step 3"}};
                        Notes = new List<string> {"Here be a note", "and another one you scurvey dog!"};
                        ControllerName = "selling";
                        break;

                    case "chain":
                        Title = "Buying & Selling Within a Chain";
                        Summary = "Here is a complete outline of the basic processes involved when buying and selling a property within a chain. Whilst it doesn't include all of the technicalities that could arise, or help you fill out any forms, it is enough to show you what to expect when acting as your own legal representative within a property buying & selling chain.";
                        LastUpdated = new DateTime(2014, 08, 28);
                        ProcessDictionary = new Dictionary<int, string> {{1, "Step 1"}, {2, "Step 2"}, {3, "Step 3"}};
                        Notes = new List<string> {"Here be a note", "and another one you scurvey dog!"};
                        ControllerName = "chain";
                        break;

                    default:
                        Title = "Selling Process";
                        Summary = "Here is a complete outline of the basic selling process, from start to finish, for a registered property in the UK. Whilst it doesn't include all of the technicalities that need to be considered, or help you fill out any forms, it is enough to show you what to expect when selling a house without using a solicitor or estate agent.";
                        LastUpdated = new DateTime(2014, 08, 28);
                        ProcessDictionary = new Dictionary<int, string> {{1, "Step 1"}, {2, "Step 2"}, {3, "Step 3"}};
                        Notes = new List<string> {"Here be a note", "and another one you scurvey dog!"};
                        ControllerName = "selling";
                        break;
                }
                return this;
            }
        }

        public class Start
        {
            

            public Start()
            {
            }
        }
    }
}
