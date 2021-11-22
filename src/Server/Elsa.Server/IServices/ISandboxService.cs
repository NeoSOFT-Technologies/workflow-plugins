using Elsa.Server.Models.Sandbox;
using System.Threading.Tasks;

namespace Elsa.Server.IServices
{
    public interface ISandboxService
    {
        Task<AuthenticateResponse> AuthenticateAsync();
        Task<BaseResponse<PanBasicData>> VerifyPanAsync(VerifyPanRequest request, string accessToken);
        Task<BaseResponse<BankAccountData>> VerifyBankAccountAsync(VerifyBankAccountRequest request, string accessToken);
    }
}
