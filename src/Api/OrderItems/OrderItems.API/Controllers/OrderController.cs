using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderItems.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrderItems.API.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly Random _random = new Random();
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(Order order)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(order);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("http://elsaserver:80/PlaceOrder", httpContent);
            }
            return Ok();
        }

        [HttpPost]
        public ActionResult<Order> Catalogue(Order order)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(1, "Shoes");
            dic.Add(2, "Mobiles");
            dic.Add(3, "Tablets");
            dic.Add(4, "Bottles");
            dic.Add(5, "Fans");

            foreach (var item in order.Items)
            {
                item.ItemName = dic[item.ItemId];
                item.Quantity = _random.Next(1000);
            }
            return Ok(order);
        }

        [HttpPost]
        public ActionResult<Order> Inventory(Order order)
        {
            foreach (var item in order.Items)
            {
                item.IsAvailable = _random.Next(2) == 0;
            }
            return Ok(order);
        }

        [HttpPost]
        public ActionResult<OrderItem> Order(OrderItem item)
        {
            item.ItemOrderId = Guid.NewGuid().ToString();
            item.Discount = _random.Next(100);
            return Ok(item);
        }
    }
}
