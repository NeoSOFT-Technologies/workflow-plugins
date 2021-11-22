namespace Elsa.Server.Models.Sandbox
{
    public class VerifyBankAccountRequest
    {
        public string Ifsc { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
    }
}
