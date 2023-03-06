using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecPortal.UnitTestWithDb
{
    public class DbHelper
    {
        public void PrepareCleanDb()
        {
            string connectionString = "";
            string newDbName = "SEC_UnitTestWithDB_" + DateTime.Now.ToString("yyyy-MM-dd_HHmm");
        }
    }
}
