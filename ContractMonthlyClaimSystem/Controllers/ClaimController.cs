using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using ContractMonthlyClaimSystem.Data;
using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;
namespace ContractMonthlyClaimSystem.Controllers
{
    public class ClaimController : Controller
    {
        private readonly DatabaseConnection db = new DatabaseConnection();
        [HttpGet]
        public IActionResult SubmitClaim()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SubmitClaim(Claim claim)
        {
            try
            {
                using (SqlConnection con = db.GetConnection())
                {
                    string query = "INSERT INTO Claims (claimType, claimDescription, LecturerName, HoursWorked, HourlyRate, Notes, Status, DocumentPath, claimDate) " +
                                   "VALUES (@Type, @Description, @LecturerName, @HoursWorked, @HourlyRate, @Notes, @Status, @DocumentPath, @ClaimDate)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@LecturerId", claim.LecturerId);
                    cmd.Parameters.AddWithValue("@HoursWorked", claim.HoursWorked);
                    cmd.Parameters.AddWithValue("@HourlyRate", claim.HourlyRate);
                    cmd.Parameters.AddWithValue("@Notes", claim.Notes ?? (object)DBNull.Value);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                TempData["Message"] = "Claim submitted successfully.";
                return RedirectToAction("ViewClaims");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
                return View();
            }

        }
        public IActionResult ViewClaims()
        {
            List<Claim> claims = new List<Claim>();
            using (SqlConnection con = db.GetConnection())
            {
                string query = "SELECT * FROM Claims";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    claims.Add(new Claim
                    {
                        claimId = Convert.ToInt32(reader["ClaimId"]),
                        LecturerId = Convert.ToInt32(reader["LecturerId"]),
                        HoursWorked = Convert.ToDouble(reader["HoursWorked"]),
                        HourlyRate = Convert.ToDouble(reader["HourlyRate"]),
                        Notes = reader["Notes"].ToString(),
                        Status = reader["Status"].ToString()
                    });
                }
            }
            return View(claims);
        }

        // ✅ Approve/Reject Claim
        [HttpPost]
        public IActionResult ApproveClaim(int claimId, string actionType)
        {
            string status = actionType == "Approve" ? "Approved" : "Rejected";

            using (SqlConnection con = db.GetConnection())
            {
                string query = "UPDATE Claims SET Status=@Status WHERE ClaimId=@ClaimId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@ClaimId", claimId);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            TempData["Message"] = $"Claim {status} successfully!";
            return RedirectToAction("VerifyClaims");
        }

    
        public IActionResult VerifyClaims()
        {
            return View(ViewClaims());
        }

        //Upload Documents
        [HttpGet]
        public IActionResult UploadDocuments()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadDocuments(IFormFile file, int claimId)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    string filePath = Path.Combine(uploadsFolder, file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    using (SqlConnection con = db.GetConnection())
                    {
                        string query = "UPDATE Claims SET DocumentPath=@DocumentPath WHERE ClaimId=@ClaimId";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@DocumentPath", "/uploads/" + file.FileName);
                        cmd.Parameters.AddWithValue("@ClaimId", claimId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }

                    TempData["Message"] = "File uploaded successfully!";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
            }

            return RedirectToAction("ViewClaims");

        }
    }
}
