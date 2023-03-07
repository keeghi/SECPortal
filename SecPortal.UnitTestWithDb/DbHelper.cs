using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecPortal.UnitTestWithDb
{
    public static class DbHelper
    {
        public static string PrepareCleanDb()
        {
            string newDbName = "SEC_UnitTestWithDB_" + DateTime.Now.ToString("yyyy-MM-dd_HHmm");
            string connectionString = $"Server=MSI;Database={newDbName};Trusted_Connection=True;MultipleActiveResultSets=true";

            return connectionString;
        }
    }
}
