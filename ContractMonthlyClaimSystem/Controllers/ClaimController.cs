using Microsoft.AspNetCore.Mvc;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class ClaimController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
