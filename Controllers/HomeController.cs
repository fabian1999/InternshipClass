using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RazorMvc.Data;
using RazorMvc.Models;
using RazorMvc.Services;

namespace RazorMvc.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IInternshipService internshipService;
        private readonly MessageService messageService;

        public HomeController(ILogger<HomeController> logger, IInternshipService internshipService, MessageService messageService)
        {
            this.internshipService = internshipService;
            _logger = logger;
            this.messageService = messageService;
        }

        public IActionResult Index()
        {
            return View(internshipService.GetMembers());
        }

        public IActionResult Privacy()
        {
            var interns = internshipService.GetMembers();
            return View(interns);
        }

        public IActionResult Chat()
        {
            return View(messageService.GetAllMessages());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpDelete]
        public void RemoveMember(int index)
        {
            internshipService.RemoveMember(index);
        }

        [HttpGet]
        public Intern AddMember(string memberName)
        {
            Intern intern = new Intern();
            intern.Name = memberName;
            intern.DateOfJoin = DateTime.Now;
            return internshipService.AddMember(intern);
        }

        [HttpPut]
        public void UpdateMember(int id, string memberName)
        {
            Intern intern = new Intern();
            intern.Id = id;
            intern.Name = memberName;
            intern.DateOfJoin = DateTime.Now;
            internshipService.UpdateMember(intern);
        }
    }
}
