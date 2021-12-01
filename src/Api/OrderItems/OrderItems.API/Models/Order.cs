using System;
using System.Collections.Generic;

namespace OrderItems.API.Models
{
    public class Order
    {
        public DateTime OrderCreatedON { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
