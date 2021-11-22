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
                var url = $"{_sandboxSettings.BaseUrl}/authenticate";
                HttpResponseMessage httpResponse = await httpClient.PostAsync(url, null);
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<AuthenticateResponse>(responseString);
            }
            return response;
        }

        public async Task<BaseResponse<PanBasicData>> VerifyPanAsync(VerifyPanRequest request, string accessToken)
        {
            var response = new BaseResponse<PanBasicData>();
            using (HttpClient httpClient = new HttpClient())
            {
                AddRequestHeaders(httpClient, accessToken);
                var url = $"{_sandboxSettings.BaseUrl}/pans/{request.PanNumber}/verify?consent={request.Consent}&reason={request.Reason}";
                HttpResponseMessage httpResponse = await httpClient.GetAsync(url);
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<BaseResponse<PanBasicData>>(responseString);
            }
            return response;
        }

        public async Task<BaseResponse<BankAccountData>> VerifyBankAccountAsync(VerifyBankAccountRequest request, string accessToken)
        {
            var response = new BaseResponse<BankAccountData>();
            using (HttpClient httpClient = new HttpClient())
            {
                AddRequestHeaders(httpClient, accessToken);
                var url = $"{_sandboxSettings.BaseUrl}/bank/{request.Ifsc}/accounts/{request.AccountNumber}/verify?name={request.Name}&mobile={request.Mobile}";
                HttpResponseMessage httpResponse = await httpClient.GetAsync(url);
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<BaseResponse<BankAccountData>>(responseString);
            }
            return response;
        }

        private void AddRequestHeaders(HttpClient httpClient, string accessToken)
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);
            httpClient.DefaultRequestHeaders.Add("x-api-key", _sandboxSettings.ApiKey);
            httpClient.DefaultRequestHeaders.Add("x-api-version", _sandboxSettings.ApiVersion);
        }
    }
}