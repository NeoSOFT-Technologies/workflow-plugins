using Entities;
using IRepository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CustomerAccountAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost("SubmitDetails")]
        public async Task<UserDetails> SubmitDetails(UserDetails user)
        {
            var response = await _userRepository.AddAsync(user);

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(response);

                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                //HttpResponseMessage response1 = client.PostAsync("http://localhost:8054/PostEntity", httpContent).Result;
                HttpResponseMessage response1 = client.PostAsync("https://localhost:5001/PostEntity", httpContent).Result;
            }
            return response;
        }



        [HttpPut("AssignAccountNumber")]
        public async Task<UserDetails> AssignAccountNumber(UserDetails user)
        {
            System.Random random = new System.Random();
            user.AccountNumber = Convert.ToString(random.Next(100000001));
            await _userRepository.UpdateAsync(user);

            return user;
        }
        [HttpDelete("DeleteApplication")]
        public async Task<ActionResult> Delete(string id)
        {
            var userId = new Guid(id);
            var userToDelete = await _userRepository.GetByIdAsync(userId);
            await _userRepository.DeleteAsync(userToDelete);
            return NoContent();
        }
    }
}
