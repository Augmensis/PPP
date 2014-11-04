using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Management
{
    public enum ProcessType
    {
        Process = 0,
        Caveat = 1,
        Note = 2
    }

    public class Process 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; protected set; }
        public int ProductId { get; protected set; }
        public int Position { get; protected set; }
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public List<String> Notes { get; protected set; }
        public ProcessType ProcessType { get; protected set; }

        public DateTime CreationDate { get; protected set; }
        public DateTime LastUpdated { get; protected set; }
        public bool IsDeleted { get; protected set; }


        public Process()
        {
            Position = 0;
            Title = "";
            Description = "";
            LastUpdated = new DateTime(2000, 1, 1);
            ProcessType = ProcessType.Process;
            IsDeleted = false;
        }
    }
}
