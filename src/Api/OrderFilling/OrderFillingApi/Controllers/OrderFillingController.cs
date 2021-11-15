using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderFilling.Entities;
using OrderFilling.IRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrderFillingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderFillingController : Controller
    {
        private readonly IOrderRepository _OrderRepository;
        private readonly IUserRepository _userRepository; 
        public OrderFillingController(IOrderRepository OrderRepository,IUserRepository userRepository)
        {
            _OrderRepository = OrderRepository;
            _userRepository = userRepository;
        }
  

        [HttpPost("AddOrder")]
        public async Task<Order> CreateOrder(Order order)
        {
            order.OrderDateTime = DateTime.Now;
            var dtos = await _OrderRepository.AddOrderAsync(order);

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(dtos);

                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response1 = client.PostAsync("https://localhost:5001/PostEntity2", httpContent).Result;
            }

            return dtos;
        }

        [HttpGet("CheckCredit")]
         public async Task<ActionResult<bool>> CheckCredit(string ProductId,string id)
         {
            var userid = new Guid(id);
            var Product = new Guid(ProductId);
            var creditResult = await _userRepository.GetUsersByIdAsync(userid,Product);
            return Ok(creditResult);
         }

        [HttpGet("CheckInventory")]
        public async Task<ActionResult<bool>> CheckInventory(Guid ProductId)
        {
            //var productid = new Guid(ProductId);
            var checkInv =  await _userRepository.GetProductInventoryById(ProductId);
            return Ok(checkInv);
        }

        [HttpGet("ScheduleShipment")]
        public async Task<ActionResult> ShipmentSchedule(Guid OrderId)
        {
            var order = await _userRepository.GetById(OrderId);
            order.DeliveryBy = "Wednesday";
            var scheduleTime = await _userRepository.ScheduleOrder(order);
            var order2 = await _userRepository.GetById(OrderId);
            return Ok(order2);
       
        }

        [HttpGet("ShipPrdouct")]
        public async Task<ActionResult> shipProduct(Guid OrderId)
        {
            var order = await _userRepository.GetById(OrderId);
            order.DeliveryStatus = "shipped";
            return Ok(order);
        }

        [HttpGet("prepareBill")]
        public async Task<ActionResult<Order>> GetBill(Guid OrderId,Guid ProductId)
        {
            var products = await _userRepository.GetProductsById(ProductId);
            await _userRepository.ReduceInventory(products);
            var order = await _userRepository.GetById(OrderId);
            return order;
        }
        [HttpGet("CheckMaterialInventory")]
        public async Task<ActionResult<Products>> CheckMaterialInventory(Guid Id)
        {
            var products = await _userRepository.GetProductsById(Id);
            return products;
        }

        [HttpGet("OrderMaterial")]
        public async Task<ActionResult> OrderMaterial()
        {
            return Ok(true);
        }
        
        [HttpGet("GetUserDetail")]
        public async Task<ActionResult<Users>> GetUserDetail(Guid Id)
        {
            var userDetail = await _userRepository.GetUserById(Id);
            return Ok(userDetail);
        }
    }
}
