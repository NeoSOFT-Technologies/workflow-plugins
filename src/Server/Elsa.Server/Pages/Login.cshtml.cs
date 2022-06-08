using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Elsa.Server.Pages
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var username = Request.Form["username"];
            var password = Request.Form["password"];
            string endPoint = "http://192.168.1.103:9990/auth/realms/tyk/protocol/openid-connect/token";
            var client = new HttpClient();

            var data = new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("client_id", "tyk-client"),
                    new KeyValuePair<string, string>("client_secret", "Mqy0HM9CFsoDBVT1ZNhY2O9uYTYpZ28m"),
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password),
                };
            var response = client.PostAsync(endPoint, new FormUrlEncodedContent(data)).GetAwaiter().GetResult();
            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            var accessToken = responseJson.Value<string>("access_token");
            HttpContext.Session.SetString("Access Token", accessToken);
            return Redirect("/");
        }
    }
}
