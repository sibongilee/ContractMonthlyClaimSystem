using Microsoft.AspNetCore.Mvc;
using ContractMonthlyClaimSystem.Models;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class ClaimController : Controller
    {
        public static List<Claim> claims = new List<Claim>();
        public static List<User> users = new List<User>();
        public static List<Lecturer> lecturers = new List<Lecturer>();
        [HttpGet]
        public IActionResult Submit()
        {
            ViewBag.Users = users;
            ViewBag.Lecturers = lecturers;
            return View();
        }
        [HttpPost]
        public IActionResult Submit(Claim claim)
        {
            claim.claimId = claims.Count + 1;
            claims.Add(claim);
            TempData["Success"]= "Claim Submitted Successfully.";
            TempData.Keep("User_Name");
            TempData.Keep("User_Role");
            return RedirectToAction("Index", "Dashboard");
        }
        [HttpGet]
        public IActionResult UploadDocuments() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult UploadDocuments(int claimId, string documentPath)
        {
            var claim = claims.FirstOrDefault(claims => claims.claimId == claimId);
            if (claim != null)
            {
                claim.Documents = documentPath;
                TempData["Success"] = "Document Uploaded Successfully.";

                TempData.Keep("User_Name");
                TempData.Keep("User_Role");
                return RedirectToAction("Index", "Dashboard");
            }
            return NotFound();
        }
        
        [HttpPost]
        public IActionResult TrackStatus()
        { 
        var lecturerName = TempData["User_Name"]?.ToString();
        var lecturerClaims = claims.Where(c => c.LecturerName == lecturerName).ToList();
        TempData.Keep("User_Name");
        TempData.Keep("User_Role");
        return View(lecturerClaims);
        }
        [HttpGet]
        public IActionResult VerifyClaims()
        { 
         var role = TempData["User_Role"]?.ToString();
            if(role != "Programme Coordinator" && role != "Academic Manager")
            {
                TempData["Error"] = "You do not have permission to verify claims.";
                TempData.Keep("User_Name");
                TempData.Keep("User_Role");
                return RedirectToAction("Index", "Dashboard");
            }
            var claim = claims.FirstOrDefault(c => c.Status == "Submitted");
            if (claim != null)
            {
                claim.Status = "Verified";
                TempData["Message"] = "Claim verified successfully.";
                TempData.Keep("User_Name");
                TempData.Keep("User_Role");
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }

        [HttpGet]
        public IActionResult ApproveClaim(int id)
        {
            var role = TempData["User_Role"]?.ToString();
            if (role != "Programme Coordinator" && role != "Academic Manager")
            {
                TempData["Error"] = "You do not have permission to approve claims.";
                TempData.Keep("User_Name");
                TempData.Keep("User_Role");
                return RedirectToAction("Index", "Dashboard");
            }
            var claim = claims.FirstOrDefault(c => c.claimId == id);
            if (claim != null)
            {
                claim.Status = "Approved";
                TempData["Message"] = "Claim approved successfully.";
                TempData.Keep("User_Name");
                TempData.Keep("User_Role");
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }
        [HttpGet]
        public IActionResult ViewClaims()
        {
            return View(claims);
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
