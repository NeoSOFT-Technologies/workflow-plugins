using System;

namespace InvoiceCustomerApi.Entities
{
    public class Invoice
    {
        public Guid InvoiceId { get; set; }
        public Guid ClientId { get; set; }
        public decimal Amount { get; set; }
        public int NumberofResource { get; set; }
        public DateTime Date { get; set; }
        public bool IsInvoicePaid { get; set; }

        public Client Client { get; set; }
    }
}
