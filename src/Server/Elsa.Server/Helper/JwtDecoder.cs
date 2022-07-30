using System.IdentityModel.Tokens.Jwt;

namespace Elsa.Server.Helper
{
    public static class JwtDecoder
    {
        public static JwtSecurityToken DecodeToken(string accessToken)
        {
            string stream = accessToken;
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(stream);
            return jwtToken;
        }
    }
}
