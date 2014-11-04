using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Data;

namespace Services.Management
{
    public class Note
    {
        private const string SQL_NoteFetchAll = "";
        private const string SQL_NoteFetchOne = "";
        private const string SQL_NoteSave = "";


        public int Id { get; protected set; }
        public int ProductId { get; protected set; }
        public int ProcessId { get; protected set; }
        public int Position { get; protected set; }
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public DateTime CreationDate { get; protected set; }
        public DateTime LastUpdated { get; protected set; }
        public bool IsDeleted { get; protected set; }


        public Note()
        {
            Id = 0;
            ProductId = 0;
            ProcessId = 0;
            Position = 0;
            CreationDate = DateTime.UtcNow;
            LastUpdated = DateTime.UtcNow;
            IsDeleted = false;
        }

        public static List<Note> FetchAllNotes(int productId)
        {
            try
            {
                // Select * from citizenDB.Processes where ProductId = @productId
                var dt = Connection.GetMySqlTable(SQL_NoteFetchAll, new object[] { productId });

                var noteList = new List<Note>();

                foreach (DataRow process in dt.AsEnumerable())
                {
                    var tempProcess = new Note
                    {
                        Id = process.Field<int>("Id"),
                        ProductId = process.Field<int>("ProductId"),
                        ProcessId = process.Field<int>("ProductId"),
                        Position = process.Field<int>("Position"),
                        Title = process.Field<string>("Title"),
                        Description = process.Field<string>("Description"),
                        CreationDate = process.Field<DateTime>("CreationDate"),
                        LastUpdated = process.Field<DateTime>("LastUpdated"),
                        IsDeleted = process.Field<bool>("IsDeleted")
                    };
                    noteList.Add(tempProcess);
                }

                return noteList;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error with Fetching all Processes: {0}", ex.Message));
            }

        }

        public static Note FetchOneNote(int productId, int processId)
        {
            try
            {
                var dt = Connection.GetMySqlTable(SQL_NoteFetchOne, new object[] { productId });

                var tempNote = new Note
                {
                    Id = dt.Rows[0].Field<int>("Id"),
                    ProductId = dt.Rows[0].Field<int>("ProductId"),
                    ProcessId = dt.Rows[0].Field<int>("ProductId"),
                    Position = dt.Rows[0].Field<int>("Position"),
                    Title = dt.Rows[0].Field<string>("Title"),
                    Description = dt.Rows[0].Field<string>("Description"),
                    CreationDate = dt.Rows[0].Field<DateTime>("CreationDate"),
                    LastUpdated = dt.Rows[0].Field<DateTime>("LastUpdated"),
                    IsDeleted = dt.Rows[0].Field<bool>("IsDeleted")
                };

                return tempNote;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error with Fetching all Processes: {0}", ex.Message));
            }

        }

        public bool Save(Note acc)
        {
            try
            {
                Connection.ExcecuteMySql(SQL_NoteSave);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error trying to save Process changes: {0}", ex.Message));
            }
            return false;
        }
    }
}
