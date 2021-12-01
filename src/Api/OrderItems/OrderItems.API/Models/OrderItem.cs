using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderItems.API.Models
{
    public class OrderItem
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public bool IsAvailable { get; set; }
        public string ItemOrderId { get; set; }
        public int Discount { get; set; }
    }
}
