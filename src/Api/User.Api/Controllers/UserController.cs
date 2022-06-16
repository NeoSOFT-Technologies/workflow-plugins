using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using User.Api.Models;
using User.Api.Services;

namespace User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public ActionResult<List<ApplicationUser>> Get() =>
            _userService.Get();

        [HttpGet("{id:length(24)}", Name = "GetApplicationUser")]
        public ActionResult<ApplicationUser> Get(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public ActionResult<ApplicationUser> Create(ApplicationUser user)
        {
            var response = _userService.Create(user);

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(response);

                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response1 = client.PostAsync("http://localhost:8054/CreateUser", httpContent).Result;
            }

            return CreatedAtRoute("GetApplicationUser", new { id = user.Id.ToString() }, user);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, ApplicationUser userIn)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            userIn.IsActive = true;
            _userService.Update(id, userIn);

            return Ok(userIn);
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _userService.Remove(user.Id);

            return NoContent();
        }
    }
}
