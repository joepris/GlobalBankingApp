using Microsoft.AspNetCore.Mvc;

namespace GlobalBankingApp.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string email, string password)
        {
            // For now, accept any email and password
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ErrorMessage = "Invalid login attempt.";
            return View();
        }

    }
}

