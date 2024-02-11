using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorExpenseTracker.Data
{
    public class SqlConfiguration
    {
        public string ConnectionString { get; }

        public SqlConfiguration(string connectionString) => ConnectionString = connectionString;

    }
}
