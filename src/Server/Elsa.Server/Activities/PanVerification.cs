using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Expressions;
using Elsa.Server.IServices;
using Elsa.Server.Models.Sandbox;
using Elsa.Services;
using Elsa.Services.Models;
using System;
using System.Threading.Tasks;

namespace Elsa.Server.Activities
{
    [Action(
        Category = "Custom",
        DisplayName = "PAN Verification",
        Description = "Verify PAN Number",
        Outcomes = new[] { "Success", "Failed" }
    )]
    public class PanVerification : Activity
    {
        private readonly ISandboxService _sandboxService;
        public PanVerification(ISandboxService sandboxService)
        {
            _sandboxService = sandboxService;
        }

        [ActivityInput(
            Label = "PAN Number",
            Hint = "PAN Number to verify",
            SupportedSyntaxes = new [] { SyntaxNames.Literal, SyntaxNames.Liquid, SyntaxNames.JavaScript }
        )]
        public string PanNumber { get; set; }

        [ActivityInput(
            Label = "Consent",
            Hint = "Y if you have consent from PAN Card Holder",
            SupportedSyntaxes = new[] { SyntaxNames.Literal, SyntaxNames.Liquid, SyntaxNames.JavaScript }
        )]
        public string Consent { get; set; }

        [ActivityInput(
            Label = "Reason",
            Hint = "Reason for PAN Card Verification",
            SupportedSyntaxes = new[] { SyntaxNames.Literal, SyntaxNames.Liquid, SyntaxNames.JavaScript }
        )]
        public string Reason { get; set; }

        [ActivityOutput(
            Hint = "PAN Verification Response"
        )]
        public PanBasicData Output { get; set; } = default!;

        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            AuthenticateResponse authenticateResponse = await _sandboxService.AuthenticateAsync();
            VerifyPanRequest request = new VerifyPanRequest()
            {
                PanNumber = PanNumber,
                Consent = Consent,
                Reason = Reason
            };
            BaseResponse<PanBasicData> response = await _sandboxService.VerifyPanAsync(request, authenticateResponse.Access_token);
            Output = response.Data;
            if (Output is not null && Output.Status.ToLower().Equals("valid"))
            {
                return Outcome("Success");
            }
            else
            {
                return Outcome("Failed");
            }
        }
    }
}
