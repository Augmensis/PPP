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
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Position { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ProcessType ProcessType { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsDeleted { get; set; }


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
