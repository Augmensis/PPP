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
        private MySql.Data.MySqlClient.MySqlConnection conn;
        private const string BASE_CONNECTION = "server=127.0.0.1;uid=user;pwd=password;database=database;";
        private const int SQL_DEFAULT_TIMEOUT = 60;

        public MySqlConnection OpenConnection(string connectionString = BASE_CONNECTION)
        {
            try
            {
                conn = new MySqlConnection {ConnectionString = connectionString};
                conn.Open();
                return conn;
            }
            catch (MySqlException ex)
            {
                //Email connection toubles to dev
                throw new Exception("Connection Fail:", ex);
             }
        }

        public DataTable GetMySqlTable(string sql, object[] commandParams = null, int timeout = SQL_DEFAULT_TIMEOUT)
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

        private MySqlCommand MySQLCommandBuilder(string sql, object[] commandParams, int timeout = SQL_DEFAULT_TIMEOUT)
        {
            var cmd = new MySqlCommand();

            if (sql.Contains("@"))
            {
                var paramNames = new List<string>();
                
                sql = Regex.Replace(sql,"\\s+", " ").Trim();
                var words = new List<string>(sql.Split(' '));

                foreach (var word in words)
                {
                    var cleanName = word.ToLower().Trim();
                    if (cleanName.StartsWith("@") && !paramNames.Contains(cleanName))
                    {
                        paramNames.Add(cleanName.TrimEnd(','));
                    }
                }

                if ((paramNames.Count > 0 && commandParams == null) || paramNames.Count != commandParams.Length)
                {
                    throw new Exception("Number of named parameters doesn't match the number of values passed!");
                }

                for (var i = 0; i < commandParams.Length; i++)
                {
                    cmd.Parameters.AddWithValue(paramNames[i], commandParams[i] ?? DBNull.Value);
                }
            }
            cmd.CommandTimeout = timeout;
            return cmd;
        }
    }
}
