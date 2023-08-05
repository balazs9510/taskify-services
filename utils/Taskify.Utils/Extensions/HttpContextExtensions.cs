using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Taskify.Utils.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetUserName(this HttpContext context)
        {
            return context?.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Actor)?.Value;
        }
    }
}
