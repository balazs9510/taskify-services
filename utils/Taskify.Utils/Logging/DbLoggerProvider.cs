using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taskify.Utils.Logging
{
    public class DbLoggerProvider : ILoggerProvider
    {
        public readonly string connectionString;

        public DbLoggerProvider(IConfiguration config)
        {
            connectionString = config.GetConnectionString("TaskifyDb");
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DbLogger(this, categoryName);
        }

        public void Dispose()
        {
           
        }
    }
}
