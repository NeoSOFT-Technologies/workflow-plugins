using Elsa.Server.IServices;
using Elsa.Server.Models.Sandbox;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Elsa.Server.Services
{
    public class SandboxService : ISandboxService
    {
        public SandboxSettings _sandboxSettings { get; }
        public SandboxService(IOptions<SandboxSettings> sandboxSettings)
        {
            _sandboxSettings = sandboxSettings.Value;
        }

        public async Task<AuthenticateResponse> AuthenticateAsync()
        {
            var response = new AuthenticateResponse();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("x-api-key", _sandboxSettings.ApiKey);
                httpClient.DefaultRequestHeaders.Add("x-api-secret", _sandboxSettings.Secret);
                httpClient.DefaultRequestHeaders.Add("x-api-version", _sandboxSettings.ApiVersion);
                HttpResponseMessage httpResponse = await httpClient.PostAsync(_sandboxSettings.BaseUrl + "/authenticate", null);
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<AuthenticateResponse>(responseString);
            }
            return response;
        }

        public async Task<PanBasicResponse> VerifyPanAsync(VerifyPanRequest request, string accessToken)
        {
            var response = new PanBasicResponse();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);
                httpClient.DefaultRequestHeaders.Add("x-api-key", _sandboxSettings.ApiKey);
                httpClient.DefaultRequestHeaders.Add("x-api-version", _sandboxSettings.ApiVersion);
                HttpResponseMessage httpResponse = await httpClient.GetAsync(_sandboxSettings.BaseUrl + "/pans/" + request.PanNumber + "/verify?consent=" + request.Consent + "&reason=" + request.Reason);
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<PanBasicResponse>(responseString);
            }
            return response;
        }
    }
}