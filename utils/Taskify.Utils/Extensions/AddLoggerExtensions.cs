using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskify.Utils.Services;

namespace Taskify.Utils.Extensions
{
    public static class AddLoggerExtensions
    {
        public static void AddLogger(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ILoggerService, LoggerService>();
        }
    }
}
