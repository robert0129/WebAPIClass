using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace Demo1AttributeRouting.CustomHandler
{
    public class CacheActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var method = context.HttpContext.Request.Method;
            if (string.Equals(method, "Get", StringComparison.OrdinalIgnoreCase))
            {
                context.HttpContext.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue()
                {
                     Public = true,
                     MaxAge = TimeSpan.FromSeconds(10)
                };
                context.HttpContext.Response.Headers[HeaderNames.Vary] = new string[] {"Accept-Encoding"};
            }
            base.OnActionExecuted(context);
        }
    }
}
