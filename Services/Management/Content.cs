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
                        ProcessDictionary = GetSellingProcessOverview();
                        Notes = new List<string> {"Here be a note", "and another one you scurvey dog!"};
                        ControllerName = "buying";
                        break;

                    case "sell":
                        Title = "Selling Process";
                        Summary = "Here is a complete outline of the basic selling process, from start to finish, for a registered property in the UK. Whilst it doesn't include all of the technicalities that need to be considered, or help you fill out any forms, it is enough to show you what to expect when selling a house without using a solicitor or estate agent.";
                        LastUpdated = new DateTime(2014, 08, 28);
                        ProcessDictionary = GetSellingProcessOverview();
                        Notes = new List<string> {"Here be a note", "and another one you scurvey dog!"};
                        ControllerName = "selling";
                        break;

                    case "chain":
                        Title = "Buying & Selling Within a Chain";
                        Summary = "Here is a complete outline of the basic processes involved when buying and selling a property within a chain. Whilst it doesn't include all of the technicalities that could arise, or help you fill out any forms, it is enough to show you what to expect when acting as your own legal representative within a property buying & selling chain.";
                        LastUpdated = new DateTime(2014, 08, 28);
                        ProcessDictionary = GetChainProcessOverview();
                        Notes = new List<string> {"Here be a note", "and another one you scurvey dog!"};
                        ControllerName = "chain";
                        break;

                    default:
                        Title = "Selling Process";
                        Summary = "Here is a complete outline of the basic selling process, from start to finish, for a registered property in the UK. Whilst it doesn't include all of the technicalities that need to be considered, or help you fill out any forms, it is enough to show you what to expect when selling a house without using a solicitor or estate agent.";
                        LastUpdated = new DateTime(2014, 08, 28);
                        ProcessDictionary = GetSellingProcessOverview();
                        Notes = new List<string> {"Here be a note", "and another one you scurvey dog!"};
                        ControllerName = "selling";
                        break;
                }
                return this;
            }

            protected Dictionary<int, string> GetSellingProcessOverview()
            {
                var tempDictionary = new Dictionary<int, string>();
                var i = 1;
                tempDictionary.Add(i,"Send Form OC1 (or OC2 if neccesary) for Office Copies.");
                tempDictionary.Add(++i,"Obtain a copy of the Title Plan.");
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
                tempDictionary.Add(++i, "Things");
                tempDictionary.Add(++i, "and stuff");
                tempDictionary.Add(++i, "and such");
                tempDictionary.Add(++i, "You've just completed the transaction. Well done!");
                return tempDictionary;
            }

            protected Dictionary<int, string> GetBuyingProcessOverview()
            {
                var tempDictionary = new Dictionary<int, string>();
                var i = 1;
                tempDictionary.Add(i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                return tempDictionary;
            }

            protected Dictionary<int, string> GetChainProcessOverview()
            {
                var tempDictionary = new Dictionary<int, string>();
                var i = 1;
                tempDictionary.Add(i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                tempDictionary.Add(++i, "");
                return tempDictionary;
            }

        }

        


    }
}
