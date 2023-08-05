using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskify.DAL;
using Taskify.DAL.Models;

namespace Taskify.Utils.Logging
{
    public class DbLogger : ILogger
    {
        private readonly DbContextOptions<TaskifyDbContext> _dbContextOptions;
        private readonly string _loggerName;
        /// <summary>
        /// Creates a new instance of <see cref="FileLogger" />.
        /// </summary>
        /// <param name="fileLoggerProvider">Instance of <see cref="FileLoggerProvider" />.</param>
        public DbLogger([NotNull] DbLoggerProvider dbLoggerProvider, string loggerName)
        {
            _dbContextOptions = new DbContextOptionsBuilder<TaskifyDbContext>()
                .UseNpgsql(dbLoggerProvider.connectionString)
                .Options;
            _loggerName = loggerName;
        }


        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                // Don't log the entry if it's not enabled.
                return;
            }
            var pair = (state as IEnumerable<KeyValuePair<string, object>>);
            var logPair = pair?.FirstOrDefault(x => x.Value is LogData);
            if (logPair?.Value is null) return;

            var logData = logPair.Value.Value as LogData;
            Task.Run(async () =>
            {
                using var dbContext = new TaskifyDbContext(_dbContextOptions);
                await dbContext.AddAsync<SystemLog>(new SystemLog
                {
                    Caller = string.Format("{0}.{1}",_loggerName, logData.CallerMember),
                    Level = logLevel,
                    Message = logData.Message,
                    UserName = logData.UserName,
                });
                await dbContext.SaveChangesAsync();
            });
        }
    }

    public class LogData
    {
        public string Message { get; set; }
        public string CallerMember { get; set; }
        public string UserName { get; set; }
    }
}
