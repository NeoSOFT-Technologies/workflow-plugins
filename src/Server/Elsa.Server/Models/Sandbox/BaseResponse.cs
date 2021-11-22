using System;

namespace Elsa.Server.Models.Sandbox
{
    public class BaseResponse<T>
    {
        public int Code { get; set; }
        public long Timestamp { get; set; }
        public Guid Transaction_id { get; set; }
        public T Data { get; set; }
    }
}
