using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Management
{
    public class Note
    {
        public int Id { get; protected set; }

        public Note()
        {
            Id = 0;
        }

        public static List<Note> Fetch(int noteId)
        {
            return new List<Note>();
        }
    }
}
