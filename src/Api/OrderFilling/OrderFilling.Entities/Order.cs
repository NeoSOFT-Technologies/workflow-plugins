using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFilling.Entities
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; } 
        public DateTime OrderDateTime { get; set; }
        public string DeliveryStatus { get; set; }
        public string DeliveryBy { get; set; }
        public int Quantity { get; set; }
    }
}
