using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceCustomerApi.Model
{
    public class InvoiceDto
    {
        public Guid InvoiceId { get; set; }
        public Guid ClientId { get; set; }
        public decimal Amount { get; set; }
        public int NumberofResource { get; set; }
        public DateTime Date { get; set; }
        public bool IsInvoicePaid { get; set; }
    }
}
