namespace Elsa.Server.Models.Sandbox
{
    public class BankAccountData
    {
        public string Reference_id { get; set; }
        public bool Account_exists { get; set; }
        public int Amount_deposited { get; set; }
        public string Message { get; set; }
        public string Name_at_bank { get; set; }
    }
}
