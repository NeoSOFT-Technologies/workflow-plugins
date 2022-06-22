using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Elsa.Server.Helper;
using System.Linq;

namespace Elsa.Server.Middleware
{
    public class Auth
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<Auth> _logger;
        public Auth(RequestDelegate next, ILogger<Auth> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
           _logger.LogInformation("Auth Middleware initiated");

            if (context.Request.Path == "/" || context.Request.Path == "/getlocalstorage")
                goto skipAuth;

            string accessToken = context.Request.Headers["Authorization"];
            if (accessToken == null )
            {
                context.Response.Redirect("/Login");
                goto skipAuth;
            }

            accessToken = accessToken.ToString().Substring("Bearer ".Length);
            if(accessToken == "undefined")
            {
                BlockRequest(context);
                goto skipAuth;
            }
            string method = context.Request.Method;
            string scope = "";
            switch (method)
            {
                case "GET":
                    scope = "view";
                    break;
                case "POST":
                    scope = "create";
                    break;
                case "PUT":
                    scope = "edit";
                    break;
                case "DELETE":
                    scope = "delete";
                    break;
                default:
                    break;
            }

            var permissionClaims = JwtDecoder.DecodeToken(accessToken).Claims.Where(x => x.Type == "permission").ToList();
            List<string> permissions = new();
            foreach (var claim in permissionClaims)
                permissions.Add(claim.Value);

            if (!permissions.Contains(scope))
                BlockRequest(context);

            skipAuth:
                await _next(context);
            
            _logger.LogInformation("Auth Middleware completed");
        }

        private void BlockRequest(HttpContext context)
        {
            var result = JsonConvert.SerializeObject("403 Not authorized");
            context.Response.OnStarting(() =>
            {
                context.Response.StatusCode = 403;
                context.Response.ContentType = "application/json";
                context.Response.ContentLength += result.Length ;
                context.Response.WriteAsync(result).GetAwaiter();
                return Task.CompletedTask;
            });
        }
        
    }
}
