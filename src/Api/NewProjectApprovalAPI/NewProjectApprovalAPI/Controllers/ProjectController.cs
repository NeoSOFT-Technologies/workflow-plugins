using NewProjectApproval.Entities;
using NewProjectApproval.IRepositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace NewProjectApprovalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        [HttpPost("AddProject")]
        public async Task<Project> Create(Project projectObj)
        {
            var response = await _projectRepository.AddAsync(projectObj);

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(response);

                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response1 = client.PostAsync("http://elsa.server:80/project-approval", httpContent).Result;
            }


            return response;
        }

        [HttpGet("GetManagerDetails")]
        public async Task<ActionResult> GetManagerDetails(string projectTechnology)
        {
            ProjectManager managerDetails = new ProjectManager();

            if (projectTechnology == ".net")
            {
                managerDetails.DeptManagerName = "Neha Haridas";
                managerDetails.DeptManagerEmail = "neha.haridas@neosoftmail.com";
                managerDetails.ITManagerName = "Vaibhav Kulkarni";
                managerDetails.ITManagerEmail = "vaibhav.kulkarni@neosoftmail.com";
            }
            else if(projectTechnology == "java")
            {
                managerDetails.DeptManagerName = "Sumit";
                managerDetails.DeptManagerEmail = "sumit@neosoftmail.com";
                managerDetails.ITManagerName = "Santosh Shinde";
                managerDetails.ITManagerEmail = "santosh.shinde@neosoftmail.com";
            }
            else
            {
                managerDetails.DeptManagerName = "Tushar";
                managerDetails.DeptManagerEmail = "tushar@neosoftmail.com";
                managerDetails.ITManagerName = "Sagar";
                managerDetails.ITManagerEmail = "sagar@neosoftmail.com";
            }
            return Ok(managerDetails);
        }
    }
}
