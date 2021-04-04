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
        private readonly InternshipService internshipService;
        private readonly InternDbContext db;

        public HomeController(ILogger<HomeController> logger, InternshipService internshipService, InternDbContext db)
        {
            this.internshipService = internshipService;
            _logger = logger;
            this.db = db;
        }

        public IActionResult Index()
        {
            return View(internshipService.GetClass());
        }

        public IActionResult Privacy()
        {
            var interns = db.Interns.ToList();
            return View(interns);
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
        public string AddMember(string member)
        {
            return internshipService.AddMember(member);
        }

        [HttpPut]
        public void UpdateMember(int index, string name)
        {
            internshipService.UpdateMember(index, name);
        }
    }
}
