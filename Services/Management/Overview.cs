using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Management
{
    public class Overview : Content
    {


        public Overview()
        {
            Title = "";
            Description = "";
            LastUpdated = new DateTime(2000, 1, 1);
            
        }

        public Overview GetOverview(string id)
        {
            switch (id)
            {
                case "buy":
                    Title = "Buying Process";
                    Description = "Here is a complete outline of the basic buying process, from start to finish, for a registered property in the UK. Whilst it doesn't include all of the technicalities that need to be considered, or help you fill out any forms, it is enough to show you what to expect when buying a house without using a solicitor or conveyancer.";
                    break;

                case "sell":
                    Title = "Selling Process";
                    Description = "Here is a complete outline of the basic selling process, from start to finish, for a registered property in the UK. Whilst it doesn't include all of the technicalities that need to be considered, or help you fill out any forms, it is enough to show you what to expect when selling a house without using a solicitor or estate agent.";
                    break;

                case "chain":
                    Title = "Buying & Selling Within a Chain";
                    Description = "Here is a complete outline of the basic processes involved when buying and selling a property within a chain. Whilst it doesn't include all of the technicalities that could arise, or help you fill out any forms, it is enough to show you what to expect when acting as your own legal representative within a property buying & selling chain.";
                    break;

                default:
                    Title = "Selling Process";
                    Description = "Here is a complete outline of the basic selling process, from start to finish, for a registered property in the UK. Whilst it doesn't include all of the technicalities that need to be considered, or help you fill out any forms, it is enough to show you what to expect when selling a house without using a solicitor or estate agent.";
                    break;
            }
            return this;
        }

        #region Seller Content
        public static Dictionary<int, string> GetSellingProcessOverview()
        {
            var dt = Services.Data.Connection.GetMySqlTable(SQL_ConnectGetSellingOverview);
            var tempDictionary = new Dictionary<int, string>();

            foreach (var content in dt.AsEnumerable())
            {
                tempDictionary.Add((int)content["Position"], content["Title"].ToString());
            }
            //LastUpdated = DateTime.Parse(dt.Rows[0]["LastUpdated"].ToString());
            return tempDictionary;
        }
        #endregion Seller Content

        #region Buyer Content
        public Dictionary<int, string> GetBuyingProcessOverview()
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
        #endregion Buyer Content

        #region Chain Content
        public Dictionary<int, string> GetChainProcessOverview()
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
        #endregion Chain Content
}
