using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Visitingcardgenerator.Models;
using Visitingcardgenerator.Data;
using System.Linq;

namespace Visitingcardgenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext db;

        // Constructor
        public HomeController(AppDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // ✅ TEST DATABASE
        public IActionResult TestDB()
        {
            var count = db.CustomerInfo.Count();
            return Content("Records: " + count);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}