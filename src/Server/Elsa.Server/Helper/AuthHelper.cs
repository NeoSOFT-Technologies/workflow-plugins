using AuthLibrary.IServices;
using AuthLibrary.Models.Request;
using AuthLibrary.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elsa.Server.Helper
{
    public class AuthHelper
    {
        private readonly IAuthService _authService;

        public AuthHelper(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<bool> Authorize(AuthorizeRequest request, string accessToken)
        {
            request.Resource = "Category";
            request.Scope = "create";
            AuthorizeResponse response = await _authService.AuthorizeAsync(request, accessToken);
            return response.IsAuthorized;
        }
        
        public async Task<List<UserPermission>> GetUserPermissionsAsync(UserPermissionsRequest request, string accessToken)
        {
            UserPermissionsResponse response = await _authService.GetUserPermissionsAsync(request, accessToken);
            if (response.IsAuthorized)
                return response.Permissions;
            return null;
        }
    }
}
