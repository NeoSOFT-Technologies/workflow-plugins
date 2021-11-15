using System;

namespace TicketProcessingRestApi.Entities
{
    public class Ticket
    {
        public Guid TicketId { get; set; }
        public string Priority { get; set; }
        public string TicketType { get; set; }
        public string createdBy { get; set; }//mail
        public DateTime CreateDate { get; set; }
        public string Discription { get; set; }
        public string Attachment { get; set; }

        public string assignManager { get; set; }//mail
        public string assignPerson { get; set; }//mail
        public DateTime assignDate { get; set; }
        public bool isResolved { get; set; }


    }
}
