using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
