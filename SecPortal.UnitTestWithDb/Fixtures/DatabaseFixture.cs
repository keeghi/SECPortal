using Microsoft.Data.SqlClient;
using SecPortal.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecPortal.UnitTestWithDb.Fixtures
{
    public class DatabaseFixture : IDisposable
    {
        private string _dbName = "";
        private bool _isDbCreated = false;

        public string DbName
        {
            get
            {
                if (string.IsNullOrEmpty(_dbName))
                {
                    _dbName = "SEC_UnitTestWithDB_" + DateTime.Now.ToString("yyyy-MM-dd_HHmm");
                }
                return _dbName;
            }
        }

        public string ConnectionString
        {
            get
            {
                string connectionString = $"Server=DESKTOP-9HD57NA\\SQLEXPRESS2019;Database={DbName};Trusted_Connection=True;MultipleActiveResultSets=true";
                return connectionString;
            }
        }

        public DatabaseFixture()
        {
            using (var db = new ApplicationDbContext(ConnectionString))
            {
                db.Database.EnsureCreated();
            }
        }

        public void Dispose()
        {
            // ... clean up test data from the database ...
        }
    }
}
