using Microsoft.AspNetCore.Mvc;
using ContractMonthlyClaimSystem.Models;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
namespace ContractMonthlyClaimSystem.Controllers
{
    public class ClaimController : Controller
    {
        //sql connection
        string connectionString = "Data Source=(localdb)\\" +
            "MSSQLLocalDB;Initial Catalog=ContractMonthlyClaimDB;Integrated Security=True;";

        [HttpGet]
        public IActionResult SubmitClaim()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SubmitClaim(Claim claim)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(
                        "INSERT INTO Claims (claimType, claimDescription, LecturerName, HoursWorked, HourlyRate, Status, Documents, UserId, claimDate) " +
                        "VALUES (@claimType, @claimDescription, @LecturerName, @HoursWorked,@Notes, @HourlyRate, @Status, @DocumentPath, @UserId, @claimDate,@Notes,'Pending')", connection);
                    command.Parameters.AddWithValue(@"claimType", claim.claimType);
                    command.Parameters.AddWithValue(@"claimDescription", claim.claimDescription);
                    command.Parameters.AddWithValue(@"LecturerName", claim.LecturerName);
                    command.Parameters.AddWithValue(@"HoursWorked", claim.HoursWorked);
                    command.Parameters.AddWithValue(@"HourlyRate", claim.HourlyRate);
                    command.Parameters.AddWithValue("@Notes", claim.Notes ?? (object)DBNull.Value);
                    command.ExecuteNonQuery();
                }
                TempData["Message"] = "Claim submitted successfully.";
                return RedirectToAction("ViewClaims");
            }
            return View(claim);
        }
        [HttpGet]
        public IActionResult UploadDocuments()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UploadDocuments(int claimId, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                var filePath = Path.Combine(uploads, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE Claims SET DocumentPath=@Path WHERE ClaimId=@Id",
                        con
                    );
                    cmd.Parameters.AddWithValue("@Path", file.FileName);
                    cmd.Parameters.AddWithValue("@Id", claimId);
                    cmd.ExecuteNonQuery();
                }

                TempData["Message"] = "File uploaded successfully!";
            }
            else
            {
                TempData["Error"] = "Please select a valid file.";
            }

            return RedirectToAction("ViewClaims");
        }

        // ---------------------- VIEW CLAIMS ----------------------
        public IActionResult ViewClaims()
        {
            List<Claim> claims = new List<Claim>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Claims", con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    claims.Add(new Claim
                    {
                        claimId = (int)reader["ClaimId"],
                        LecturerName = reader["LecturerName"].ToString(),
                        HoursWorked = (double)reader["HoursWorked"],
                        HourlyRate = (double)reader["HourlyRate"],
                        Notes = reader["Notes"].ToString(),
                        Status = reader["Status"].ToString(),
                        DocumentPath = reader["DocumentPath"].ToString()
                    });
                }
            }

            return View(claims);
        }

        // ---------------------- APPROVE OR REJECT CLAIM ----------------------
        public IActionResult ApproveClaim(int id)
        {
            UpdateClaimStatus(id, "Approved");
            TempData["Message"] = "Claim approved successfully!";
            return RedirectToAction("VerifyClaims");
        }

        public IActionResult RejectClaim(int id)
        {
            UpdateClaimStatus(id, "Rejected");
            TempData["Message"] = "Claim rejected successfully!";
            return RedirectToAction("VerifyClaims");
        }

        public IActionResult VerifyClaims()
        {
            List<Claim> claims = new List<Claim>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Claims WHERE Status='Pending'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    claims.Add(new Claim
                    {
                        claimId = (int)reader["ClaimId"],
                        LecturerName = reader["LecturerName"].ToString(),
                        HoursWorked = (double)reader["HoursWorked"],
                        HourlyRate = (double)reader["HourlyRate"],
                        Notes = reader["Notes"].ToString(),
                        Status = reader["Status"].ToString()
                    });
                }
            }

            return View(claims);
        }

        private void UpdateClaimStatus(int id, string status)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Claims SET Status=@Status WHERE ClaimId=@Id", con);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}

