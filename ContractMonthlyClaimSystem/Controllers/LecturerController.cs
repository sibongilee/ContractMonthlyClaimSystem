using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class LecturerController : Controller
    {
        public static List<Lecturer> lecturers = new List<Lecturer>();
        [HttpGet]
        public IActionResult Index()
        {
            return View(lecturers);
        }
        [HttpGet]
        public IActionResult AddLecturer()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddLecturer(Lecturer lecturer)
        {
            lecturer.Id = lecturers.Count + 1;
            lecturers.Add(lecturer);
            TempData["Success"]= "Lecturer Activity Added Successfully.";
            TempData.Keep("UserName");
            TempData.Keep("UserRole");
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult LecturerList()
        {
            return View(lecturers);
        }
    }
}
