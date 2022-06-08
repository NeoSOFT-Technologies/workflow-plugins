using AuthLibrary.IServices;
using AuthLibrary.Models.Request;
using AuthLibrary.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elsa.Server.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Hello()
        {
            return Ok("hello");
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(AuthenticateRequest request)
        {
            _logger.LogInformation($"Authentication Initiated for {request.Username}");
            AuthenticateResponse response = await _authService.AuthenticateAsync(request);
            if (!response.IsAuthenticated)
            {
                _logger.LogError($"Authentication Failed with {response.Message}");
                return Unauthorized(response);
            }
            _logger.LogInformation("Authentication Successful");
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetAccessToken()
        {
            var token = Request.Headers["Authorization"];
            return Ok();

        }
        //[HttpPost]
        //public async Task<IActionResult> Authorize(AuthorizeRequest request)
        //{
        //    _logger.LogInformation("Authorization Initiated");
        //    request.Resource = "Category";
        //    request.Scope = "create";
        //    AuthorizeResponse response = await _authService.Authorize(request);
        //    if (!response.IsAuthorized)
        //    {
        //        _logger.LogError($"Authorization Failed with {response.Message}");
        //        return StatusCode(403, response);
        //    }
        //    _logger.LogInformation("Authorization Successful");
        //    return Ok(response);
        //}

        [HttpPost]
        public async Task<IActionResult> GetUserPermissions(UserPermissionsRequest request,string accessToken)
        {
            _logger.LogInformation("GetUserPermissions Initiated");
            #region Temporary 
            if (accessToken == "This is Dummy Token")
            {
                return Ok(dummyPermission());
            }
            #endregion
            UserPermissionsResponse response = await _authService.GetUserPermissionsAsync(request,accessToken);
            if (!response.IsAuthorized)
            {
                _logger.LogError($"GetUserPermissions Failed with {response.Message}");
                return Unauthorized(response);
            }
            _logger.LogError("GetUserPermissions completed with {@UserPermissionsResponse}", response);
            return Ok(response.Permissions);
        }
        
        
        private List<UserPermission> dummyPermission()
        {
            List<UserPermission> userPermList = new();
               UserPermission perm = new();
            perm.RsName = "workflow-definitions";
            perm.Scopes = new() { "view", "create" , "edit", "delete"};

            UserPermission perm2 = new();
            perm2.RsName = "workflow-instances";
            perm2.Scopes = new() { "view", "create", "edit", "delete" };
            userPermList.Add(perm);
            userPermList.Add(perm2);
            return userPermList;
        }
    }
}
