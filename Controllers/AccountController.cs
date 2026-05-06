using Microsoft.AspNetCore.Mvc;
using Visitingcardgenerator.Data;
using System.Linq;

namespace Visitingcardgenerator.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext db;

        public AccountController(AppDbContext context)
        {
            db = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = db.Users
                .AsEnumerable()   // pulls data safely first
                .FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Role", user.Role);

                // ✅ Redirect to Upload page
                return RedirectToAction("Index", "Upload");
            }

            ViewBag.Error = "Invalid Username or Password";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}