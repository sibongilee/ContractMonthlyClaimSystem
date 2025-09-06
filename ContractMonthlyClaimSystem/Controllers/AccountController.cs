using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class AccountController : Controller
    {
        public static List<User> users = new List<User>();
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(User user)
        { 
        user.Id = users.Count + 1;
        users.Add(user);
        TempData["Success"]= "Registration Successful. Please login";
        return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password) 
        {
        var user = users.Find(u => u.Email == email && u.Password == password);
            if (user != null) 
            {
            TempData["UserName"]= user.Name;
            TempData["UserRole"]= user.Role;
            return RedirectToAction("Index", "Dashboard");
            }
            // if user is null, still allow redirect to dashboard
            TempData["UserName"]= "Guest";
            TempData["UserRole"]= "Lecturer";
            return RedirectToAction("Index", "Dashboard");

        }
        [HttpPost]
        public IActionResult Logout()
        {
            TempData.Clear();
            return RedirectToAction("Login");
        }
    }
}
