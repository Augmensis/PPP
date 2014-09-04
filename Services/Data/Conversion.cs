using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.Types;

namespace Services.Data
{
    public class Conversion
    {
        public static MySqlDateTime DateTimeToMySQL(DateTime date)
        {
            var mysqlDate = new MySqlDateTime();
            mysqlDate.Year = date.Year;
            mysqlDate.Month = date.Month;
            mysqlDate.Day = date.Day;
            mysqlDate.Hour = date.Hour;
            mysqlDate.Minute = date.Minute;
            mysqlDate.Second = date.Second;
            return mysqlDate;
        }
    }
}
