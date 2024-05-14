using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooDrink.Infrastructure.Extensions
{
    public static class HttpContextExtension
    {
        public static string GetName(this HttpContext context)
        {
            return context.User?.Claims?.SingleOrDefault(p => p.Type.Contains("name"))?.Value ?? string.Empty;
        }
    }
}
