namespace Elsa.Server.Models.Sandbox
{
    public class VerifyPanRequest
    {
        public string PanNumber { get; set; }
        public string Consent { get; set; }
        public string Reason { get; set; }
    }
}
