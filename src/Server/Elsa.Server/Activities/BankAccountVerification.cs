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
        DisplayName = "Bank Account Verification",
        Description = "Verify Bank Account",
        Outcomes = new[] { "Success", "Failed" }
    )]
    public class BankAccountVerification : Activity
    {
        private readonly ISandboxService _sandboxService;
        public BankAccountVerification(ISandboxService sandboxService)
        {
            _sandboxService = sandboxService;
        }

        [ActivityInput(
            Label = "IFSC Code",
            Hint = "IFSC Code of the branch",
            SupportedSyntaxes = new[] { SyntaxNames.Literal, SyntaxNames.Liquid, SyntaxNames.JavaScript }
        )]
        public string IFSC { get; set; }

        [ActivityInput(
            Label = "Account Number",
            Hint = "Acount Number to verify",
            SupportedSyntaxes = new[] { SyntaxNames.Literal, SyntaxNames.Liquid, SyntaxNames.JavaScript }
        )]
        public string AccountNumber { get; set; }

        [ActivityInput(
            Label = "Name",
            Hint = "Name of the account holder",
            SupportedSyntaxes = new[] { SyntaxNames.Literal, SyntaxNames.Liquid, SyntaxNames.JavaScript }
        )]
        public string AccountName { get; set; }

        [ActivityInput(
            Label = "Mobile",
            Hint = "Mobile Number of the account holder",
            SupportedSyntaxes = new[] { SyntaxNames.Literal, SyntaxNames.Liquid, SyntaxNames.JavaScript }
        )]
        public string Mobile { get; set; }

        [ActivityOutput(
            Hint = "PAN Verification Response"
        )]
        public BankAccountData Output { get; set; } = default!;

        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            AuthenticateResponse authenticateResponse = await _sandboxService.AuthenticateAsync();
            VerifyBankAccountRequest request = new VerifyBankAccountRequest()
            {
                Ifsc = IFSC,
                AccountNumber = AccountNumber,
                Name = AccountName,
                Mobile = Mobile
            };
            BaseResponse<BankAccountData> response = await _sandboxService.VerifyBankAccountAsync(request, authenticateResponse.Access_token);
            Output = response.Data;
            if (Output is not null && Output.Account_exists)
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
