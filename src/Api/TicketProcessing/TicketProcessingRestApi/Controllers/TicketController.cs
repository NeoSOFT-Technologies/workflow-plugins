using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TicketProcessingRestApi.Entities;
using TicketProcessingRestApi.IRepository.IRepository;

namespace TicketProcessingRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository ticketRepository;

        public TicketController(ITicketRepository _ticketRepository)
        {
            this.ticketRepository = _ticketRepository;
        }


        [HttpGet("GetAllTickets")]
        public async Task<ActionResult> GetAllUsers()
        {
            var dtos = (await ticketRepository.ListAllAsync()).OrderBy(x => x.CreateDate);
            return Ok(dtos);
        }



        [HttpGet("GetTicket")]
        public async Task<ActionResult> GetTicketById(string id)
        {
            var ticketId = new Guid(id);
            var TicketDetails = await ticketRepository.GetByIdAsync(ticketId);

            return Ok(TicketDetails);
        }



        [HttpPost("AddTicket")]
        public async Task<ActionResult> Create(Ticket ticketEntity)
        {
            ticketEntity.CreateDate = DateTime.Now;
            var response = await ticketRepository.AddAsync(ticketEntity);

            using (HttpClient httpClient = new HttpClient())
            {
                string JsonResult = JsonConvert.SerializeObject(response);
                StringContent stringContent = new StringContent(JsonResult, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponse = httpClient.PostAsync("https://localhost:5001/PostTicket", stringContent).Result;
            }
            return Ok(response);
        }




        [HttpPut("assignManager")]
        public async Task<ActionResult> AssignTicketToManager(Ticket ticketEntity)
        {
            TimeSpan start = new TimeSpan(10, 0, 0); //10am o'clock
            TimeSpan end = new TimeSpan(20, 0, 0); //8pm o'clock
            TimeSpan now = DateTime.Now.TimeOfDay;

            if (start <= end)
            {
                if ((now >= start) && (now <= end))
                {
                    switch (ticketEntity.TicketType)
                    {
                        case "IT":
                            ticketEntity.assignManager = "vaibhav.kulkarni@neosoftmail.com";
                            break;
                        case "HR":
                            ticketEntity.assignManager = "simran@neosoftmail.com";
                            break;
                        case "Admin":
                            ticketEntity.assignManager = "tushar@neosoftmail.com";
                            break;
                    }

                }
                else
                {
                    switch (ticketEntity.TicketType)
                    {
                        case "IT":
                            ticketEntity.assignManager = "neha.haridas@neosoftmail.com";
                            break;
                        case "HR":
                            ticketEntity.assignManager = "Shradha@neosoftmail.com";
                            break;
                        case "Admin":
                            ticketEntity.assignManager = "bhavesh@neosoftmail.com";
                            break;
                    }
                }
            }

            await ticketRepository.UpdateAsync(ticketEntity);
            return Ok(ticketEntity);
        }


        [HttpPut("AssignTicket")]
        public async Task<ActionResult> AssignTickettoPerson(Ticket ticketEntity)
        {
            ticketEntity.assignDate = DateTime.Now;
            if (ticketEntity.assignPerson == "")
            {
                TimeSpan start = new TimeSpan(10, 0, 0); //10am o'clock
                TimeSpan end = new TimeSpan(20, 0, 0); //8pm o'clock
                TimeSpan now = DateTime.Now.TimeOfDay;

                if (start <= end)
                {
                    if ((now >= start) && (now <= end))
                    {
                        switch (ticketEntity.TicketType)
                        {
                            case "IT":
                                ticketEntity.assignPerson = "vina.patil@neosoftmail.com";
                                break;
                            case "HR":
                                ticketEntity.assignPerson = "vishal@neosoftmail.com";
                                break;
                            case "Admin":
                                ticketEntity.assignPerson = "Vaibhav@neosoftmail.com";
                                break;
                        }
                    }
                    else
                    {
                        switch (ticketEntity.TicketType)
                        {
                            case "IT":
                                ticketEntity.assignPerson = "akshay@neosoftmail.com";
                                break;
                            case "HR":
                                ticketEntity.assignPerson = "bhavesh@neosoftmail.com";
                                break;
                            case "Admin":
                                ticketEntity.assignPerson = "bhavna@neosoftmail.com";
                                break;
                        }
                    }
                }
            }
            await ticketRepository.UpdateAsync(ticketEntity);
            using (HttpClient httpClient = new HttpClient())
            {
                string JsonResult = JsonConvert.SerializeObject(ticketEntity);

                StringContent stringContent = new StringContent(JsonResult, System.Text.Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Add("X-Correlation-Id", ticketEntity.TicketId.ToString());
                HttpResponseMessage httpResponse = httpClient.PutAsync("https://localhost:5001/PutTicket", stringContent).Result;
            }
            return Ok(ticketEntity);
        }



        [HttpPut("IsIssueResloved")]
        public async Task<ActionResult> IssueResolve(Ticket ticketEntity)
        {
            ticketEntity.isResolved = true;
            await ticketRepository.UpdateAsync(ticketEntity);
            return Ok(ticketEntity);
        }
    }
}
