using System.Data.SqlClient;
using System.Text;
using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using ContractMonthlyClaimSystem.Data;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class AccountController : Controller
    {
        // Database connection instance
        private readonly DatabaseConnection db = new DatabaseConnection();
        // GET: Login Page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login Verification
        [HttpPost]
        public IActionResult Login(User user)
        {
            try
            {
                using (SqlConnection con = db.GetConnection())
                {
                    string query = "SELECT * FROM Users WHERE Email=@Email AND Password=@Password";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        TempData["Username"] = reader["FullName"].ToString();
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        ViewBag.Error = "Invalid login credentials.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
            }
            return View();
        }

        // GET: Registration
        public IActionResult Registration()
        {
            return View();
        }

        // POST: Registration
        [HttpPost]
        public IActionResult Registration(User user)
        {
            try
            {
                using (SqlConnection con = db.GetConnection())
                {
                    string query = "INSERT INTO Users (FullName, Email, Password, Role) VALUES (@FullName, @Email, @Password, @Role)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@FullName", user.Name);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@Role", user.Role);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    TempData["Message"] = "Registration successful!";
                    return RedirectToAction("Login");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
            }
            return View();
        }



        // Password Encryption
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

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



