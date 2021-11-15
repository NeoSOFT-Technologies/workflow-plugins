using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFilling.Entities
{
    public class Products
    {
        [Key]
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int Inventory { get; set; }
        public bool materialInventory { get; set; }
    }
}
