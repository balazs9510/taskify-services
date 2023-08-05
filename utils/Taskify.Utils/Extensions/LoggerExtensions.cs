using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Taskify.Utils.Logging;

namespace Taskify.Utils.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogError(this ILogger logger, string? message, [CallerMemberName] string callerMemberName = "")
        {
            logger.LogError(message, new LogData { CallerMember = callerMemberName });
        }

        public static void LogInformation(this ILogger logger, string? message, [CallerMemberName] string callerMemberName = "")
        {
            logger.LogInformation("{0}", new LogData { Message = message, CallerMember = callerMemberName });
        }

        public static void LogCritical(this ILogger logger, string? message, [CallerMemberName] string callerMemberName = "")
        {
            logger.LogCritical(message, new LogData { CallerMember = callerMemberName });
        }

        public static void LogDebug(this ILogger logger, string? message, [CallerMemberName] string callerMemberName = "")
        {
            logger.LogDebug(message,  new LogData { CallerMember = callerMemberName});
        }
    }
}
