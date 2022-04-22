using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Elsa.Server.Helper;
using AuthLibrary.Models.Request;
using AuthLibrary.IServices;
using Microsoft.Extensions.Logging;
using AuthLibrary.Models.Response;

namespace Elsa.Server.Middleware
{
    public class Auth
    {
        private readonly RequestDelegate _next;
        private readonly IAuthService _authService;
        private readonly ILogger<Auth> _logger;
        public Auth(RequestDelegate next, IAuthService authService, ILogger<Auth> logger)
        {
            _next = next;
            _authService = authService;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation("Auth Middleware initiated");
            try
            {
                List<string> excludePaths = new() { "/Login", "/v1/features", "/favicon.ico", "/lib", "/_content", "/Auth" };
                foreach (string path in excludePaths)
                {
                    if (context.Request.Path.StartsWithSegments(path) || context.Request.Path == "/")
                        goto skipAuth;
                }
                AuthHelper auth = new(_authService);
                string controller;
                if (context.Request.Path.StartsWithSegments("/v1"))
                {
                    controller = context.Request.Path.ToString().Split('/')[2];
                }
                else
                {
                    controller = context.Request.Path.ToString().Split('/')[1];
                }

                string accessToken = context.Request.Headers["Authorization"];
                if(accessToken == null )
                {
                    context.Response.Redirect("/Login?"+ controller);
                    goto skipAuth;
                }

                accessToken = accessToken.ToString().Substring("Bearer ".Length);
                string method = context.Request.Method; // For Scope
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
                List<string> scopes = new List<string>();
                scopes.Add(scope);

                if (controller == "activities" || controller == "workflow-storage-providers" || controller == "workflow-channels" || controller == "workflow-registry" || controller == "workflow-providers")
                    controller = "workflow-definitions";

                bool permissionResult = await _authService.ValidateAllScopes(controller, scopes, accessToken);
                if (!permissionResult)
                {
                    context.Response.StatusCode = 403;
                    context.Response.ContentType = "application/json";
                    var result = JsonConvert.SerializeObject("403 Not authorized");
                    await context.Response.WriteAsync(result);
                }
            skipAuth:
                await _next(context);
            }

            catch (Exception ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("Auth failed,   " + ex);
                context.Response.Redirect("/Login");
            }

            _logger.LogInformation("Auth Middleware completed");
        }
        
    }
}
