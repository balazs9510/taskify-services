using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskify.DAL;
using Taskify.DAL.Models;
using Taskify.Utils.Extensions;

namespace Taskify.Utils.Services
{
    public interface ILoggerService
    {
        Task LogInfo(string message, string userName = null);
        Task LogError(string message, string userName = null);
    }

    public class LoggerService : ILoggerService
    {
        private readonly TaskifyDbContext _db;
        private readonly string _userName;
        public LoggerService(TaskifyDbContext dbContext, IHttpContextAccessor httpContext)
        {
            _db = dbContext;
            _userName = httpContext.HttpContext.GetUserName();
        }

        public async Task LogInfo(string message, string userName = null)
        {
            await SaveLogMessage(message, LogLevel.Information, userName);
        }

        public async Task LogError(string message, string userName = null)
        {
            await SaveLogMessage(message, LogLevel.Error, userName);
        }

        private async Task SaveLogMessage(string message, LogLevel level, string userName)
        {
            // var caller = new StackTrace().GetFrame(2)?.GetMethod()?.Name;
            SystemLog log = new SystemLog { Level = level, Message = message, UserName = userName ?? _userName };
            _db.SystemLogs.Add(log);
            await _db.SaveChangesAsync();
        }
    }
}