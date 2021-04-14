using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RazorMvc.Data;
using RazorMvc.Hubs;
using RazorMvc.Models;
using RazorMvc.Services;

namespace RazorMvc.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<MessageHub> hubContext;
        private readonly IInternshipService internshipService;
        private readonly MessageService messageService;

        public HomeController(ILogger<HomeController> logger, IInternshipService internshipService, IHubContext<MessageHub> hubContext, MessageService messageService)
        {
            this.internshipService = internshipService;
            _logger = logger;
            this.hubContext = hubContext;
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
    }
}
