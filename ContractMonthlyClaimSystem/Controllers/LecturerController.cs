using System.Data.SqlClient;
using ContractMonthlyClaimSystem.Data;
using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class LecturerController : Controller
    {
        private readonly DatabaseConnection db = new DatabaseConnection();
        public IActionResult Index()
        {
            List<Lecturer> lecturers = new List<Lecturer>();
            using (SqlConnection con = db.GetConnection())
            {
                string query = "SELECT * FROM Lecturers";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lecturers.Add(new Lecturer
                    {
                        LecturerId = (int)reader["LecturerID"],
                        LecturerName = reader["LecturerName"].ToString(),
                        Department = reader["Department"].ToString(),
                        Email = reader["Email"].ToString()
                    });
                }
            }
            return View(lecturers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Lecturer lecturer)
        {
            using (SqlConnection con = db.GetConnection())
            {
                string query = "INSERT INTO Lecturers (LecturerName, Department, Email) VALUES (@Name, @Dept, @Email)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", lecturer.LecturerName);
                cmd.Parameters.AddWithValue("@Dept", lecturer.Department);
                cmd.Parameters.AddWithValue("@Email", lecturer.Email);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
        public IActionResult LecturerList(Lecturer lecturer)
        {
            return RedirectToAction("Index");
        }
    }
}