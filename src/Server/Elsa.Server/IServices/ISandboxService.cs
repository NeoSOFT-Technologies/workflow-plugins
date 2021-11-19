using Elsa.Server.Models.Sandbox;
using System.Threading.Tasks;

namespace Elsa.Server.IServices
{
    public interface ISandboxService
    {
        Task<AuthenticateResponse> AuthenticateAsync();
        Task<PanBasicResponse> VerifyPanAsync(VerifyPanRequest request, string accessToken);
    }
}
