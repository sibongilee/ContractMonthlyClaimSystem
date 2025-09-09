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
            TempData["Success"] = "Registration Successful. Please login";
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
                TempData["User_Name"] = user.Name;
                TempData["User_Role"] = user.Role;
                return RedirectToAction("Index", "Dashboard");
            }
            TempData["Error"] = "Invalid email or password";
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Logout()
        {
            // Clear TempData and redirect to Login
            TempData.Remove("User_Name");
            TempData.Remove("User_Role");
            TempData.Remove("Error");
            return RedirectToAction("Login");
        }
    }
}



