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
        public IActionResult Index(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Please enter a username.";
            return View();
        }
    }
}

