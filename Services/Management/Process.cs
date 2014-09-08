using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Management
{
    class Process : Content
    {      
        public Process()
        {
            Title = "";
            Summary = "";
            LastUpdated = new DateTime(2000, 1, 1);
            ProcessDictionary = new Dictionary<int, string>();
            Notes = new List<string>();
            ControllerName = "";
        }
    }
}
