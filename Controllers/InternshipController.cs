﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RazorMvc.Hubs;
using RazorMvc.Models;
using RazorMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RazorMvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternshipController : ControllerBase
    {
        private readonly IInternshipService internshipService;
        private readonly IHubContext<MessageHub> hubContext;

        public InternshipController(IInternshipService internshipService, IHubContext<MessageHub> hubContext)
        {
            this.internshipService = internshipService;
            this.hubContext = hubContext;
        }

        // GET: api/<InternshipController>
        [HttpGet]
        public IEnumerable<Intern> Get()
        {
            return internshipService.GetMembers();
        }

        // GET api/<InternshipController>/5
        [HttpGet("{id}")]
        public Intern Get(int id)
        {
            return internshipService.GetMemberById(id);
        }

        // POST api/<InternshipController>
        [HttpPost]
        public void Post([FromBody] Intern intern)
        {
            intern.DateOfJoin = DateTime.Now;
            var newMember = internshipService.AddMember(intern);
            hubContext.Clients.All.SendAsync("AddMember", newMember.Name, newMember.Id);
        }

        // PUT api/<InternshipController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Intern intern)
        {
            intern.Id = id;
            if (intern.DateOfJoin == DateTime.MinValue)
            {
                intern.DateOfJoin = DateTime.Now;
            }

            internshipService.UpdateMember(intern);
        }

        // DELETE api/<InternshipController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            internshipService.RemoveMember(id);
            hubContext.Clients.All.SendAsync("RemoveMember", id);
        }
    }
}
