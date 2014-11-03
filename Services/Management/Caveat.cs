using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Management
{
    class Caveat
    {
        public enum enCaveatParentType
        {
            Process = 0,
            Caveat = 1
        }

        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Caveat> ChildCaveats { get; set; }

    }
}
