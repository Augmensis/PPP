using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Data;

namespace Services.Management
{
    public enum enProcessType
    {
        Process = 0,
        Caveat = 1,
        Note = 2
    }

    public class Process
    {
        private const string SQL_ProcessSave = "";
        private const string SQL_ProcessFetchAll = "Select * from citizenDB.Processes where ProductId = @productId and processId = @processId ;";
        private const string SQL_ProcessFetchOne = "Select * from citizenDB.Processes where ProductId = @productId ;";


        public int Id { get; protected set; }
        public int ProductId { get; protected set; }
        public int Position { get; protected set; }
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public int NotesId { get; protected set; }
        public List<Note> Notes { get; protected set; }
        public enProcessType ProcessType { get; protected set; }

        public DateTime CreationDate { get; protected set; }
        public DateTime LastUpdated { get; protected set; }
        public bool IsDeleted { get; protected set; }


        public Process()
        {
            ProductId = 0;
            Position = 0;
            Title = "";
            Description = "";
            NotesId = 0;
            Notes = new List<Note>();
            ProcessType = enProcessType.Process;
            LastUpdated = new DateTime(2000, 1, 1);
            IsDeleted = false;
        }

        public static List<Process> FetchAllProcesses(int productId)
        {
            try
            {
                // Select * from citizenDB.Processes where ProductId = @productId
                var dt = Connection.GetMySqlTable(SQL_ProcessFetchAll, new object[] { productId });

                var processList = new List<Process>();

                foreach (DataRow process in dt.AsEnumerable())
                {
                    var tempProcess = new Process();
                    tempProcess.Id = process.Field<int>("Id");
                    tempProcess.ProductId = process.Field<int>("ProductId");
                    tempProcess.Position = process.Field<int>("Position");
                    tempProcess.Title = process.Field<string>("Title");
                    tempProcess.Description = process.Field<string>("Description");
                    tempProcess.NotesId = process.Field<int>("NotesId");
                        if (tempProcess.NotesId != 0) tempProcess.Notes = Note.Fetch(tempProcess.NotesId);
                    tempProcess.ProcessType = (enProcessType)process.Field<int>("ProcessType");
                    tempProcess.CreationDate = process.Field<DateTime>("CreationDate");
                    tempProcess.LastUpdated = process.Field<DateTime>("LastUpdated");
                    tempProcess.IsDeleted = process.Field<bool>("IsDeleted");
                    processList.Add(tempProcess);
                }
                
                return processList;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error with Fetching all Processes: {0}", ex.Message));
            }

        }

        public static Process FetchOneProcess(int productId, int processId)
        {
            try
            {
                // Select * from citizenDB.Processes where ProductId = @productId and processId = @processId
                var dt = Connection.GetMySqlTable(SQL_ProcessFetchOne, new object[] { productId, processId });

                var tempProcess = new Process();
                tempProcess.Id = dt.Rows[0].Field<int>("Id");
                tempProcess.ProductId = dt.Rows[0].Field<int>("ProductId");
                tempProcess.Position = dt.Rows[0].Field<int>("Position");
                tempProcess.Title = dt.Rows[0].Field<string>("Title");
                tempProcess.Description = dt.Rows[0].Field<string>("Description");
                tempProcess.NotesId = dt.Rows[0].Field<int>("NotesId");
                    if (tempProcess.NotesId != 0) tempProcess.Notes = Note.Fetch(tempProcess.NotesId);
                tempProcess.ProcessType = (enProcessType)dt.Rows[0].Field<int>("ProcessType");
                tempProcess.CreationDate = dt.Rows[0].Field<DateTime>("CreationDate");
                tempProcess.LastUpdated = dt.Rows[0].Field<DateTime>("LastUpdated");
                tempProcess.IsDeleted = dt.Rows[0].Field<bool>("IsDeleted");

                return tempProcess;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error with Fetching a Process: {0}", ex.Message));
            }

        }


        public bool Save(Account acc)
        {
            try
            {
                Connection.ExcecuteMySql(SQL_ProcessSave);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error trying to save Account changes: {0}", ex.Message));
            }
            return false;
        }
    }
}

