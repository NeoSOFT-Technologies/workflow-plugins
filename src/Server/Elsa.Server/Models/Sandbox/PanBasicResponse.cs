using System;

namespace Elsa.Server.Models.Sandbox
{
    public class PanBasicResponse
    {
        public int Code { get; set; }
        public Int64 timestamp { get; set; }
        public Guid transaction_id { get; set; }
        public PanBasicData data { get; set; }
    }
}
