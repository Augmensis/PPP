using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Services.Data
{
    class Connection
    {
        private static MySql.Data.MySqlClient.MySqlConnection conn;
        //private const string BASE_CONNECTION = "server=86.184.190.185;uid=root;pwd=xen0m0rph187;database=citizenDB;Convert Zero Datetime=True;Allow Zero Datetime=True;";
        private const int SQL_DEFAULT_TIMEOUT = 60;

        private static string BASE_CONNECTION { get; set; }

        private static string GetBaseConnection()
        {
            if (Environment.MachineName == "THESIUS")
            {
                return "server=192.168.1.80;uid=root;pwd=xen0m0rph187;database=citizenDB;Convert Zero Datetime=True;Allow Zero Datetime=True;";
            }
            return "server=86.184.190.185;uid=root;pwd=xen0m0rph187;database=citizenDB;Convert Zero Datetime=True;Allow Zero Datetime=True;";
        }
        


        public static MySqlConnection OpenConnection(string connectionString = "")
        {
            try
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = GetBaseConnection();
                }
                conn = new MySqlConnection { ConnectionString = connectionString };
                conn.Open();
                return conn;
            }
            catch (MySqlException ex)
            {
                //Email connection toubles to dev
                throw new Exception(string.Format("Connection Fail:{0}", ex.Message));
            }
        }

        public static DataTable GetMySqlTable(string sql, object[] commandParams = null, int timeout = SQL_DEFAULT_TIMEOUT)
        {
            MySqlCommand cmd = MySQLCommandBuilder(sql, commandParams);
            var adp = new MySqlDataAdapter();
            var dt = new DataTable();

            cmd.Connection = OpenConnection();
            cmd.CommandTimeout = timeout;

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            adp.SelectCommand = cmd;
            adp.Fill(dt);
            cmd.Connection.Close();
            adp.Dispose();
            return dt;
        }

        public static void ExcecuteMySql(string cmd, object[] cmdParams)
        {
            var newCmd = MySQLCommandBuilder(cmd, cmdParams);
            newCmd.Connection = OpenConnection();
            newCmd.ExecuteNonQuery();
            newCmd.Connection.Close();
            newCmd.Connection.Dispose();
            newCmd.Dispose();
        }

        public static void ExcecuteMySql(string cmd)
        {
            var newCmd = new MySqlCommand(cmd) {Connection = OpenConnection()};
            newCmd.ExecuteNonQuery();
            newCmd.Connection.Close();
            newCmd.Connection.Dispose();
            newCmd.Dispose();
        }

        public static MySqlCommand MySQLCommandBuilder(string sql, object[] commandParams, int timeout = SQL_DEFAULT_TIMEOUT)
        {
            var cmd = new MySqlCommand(sql);

            if (sql.Contains("@"))
            {
                var paramNames = new List<string>();

                sql = Regex.Replace(sql, "\\s+", " ").Trim();
                var words = new List<string>(sql.Split(' '));

                foreach (var word in words)
                {
                    var cleanName = word.ToLower().Trim();
                    if (word.StartsWith("@") && !paramNames.Contains(word))
                    {
                        paramNames.Add(word.TrimEnd(','));
                    }
                }

                if ((paramNames.Count > 0 && commandParams == null) || paramNames.Count != commandParams.Length)
                {
                    throw new Exception("Number of named parameters doesn't match the number of values passed!");
                }

                for (var i = 0; i < commandParams.Length; i++)
                {
                    cmd.Parameters.AddWithValue(paramNames[i], commandParams[i] ?? DBNull.Value);
                    //cmd.CommandText = cmd.CommandText.Replace(paramNames[i], commandParams[i].ToString());
                }
            }
            cmd.CommandTimeout = timeout;
            return cmd;
        }
    }
}
