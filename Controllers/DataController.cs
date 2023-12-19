using Microsoft.AspNetCore.Mvc;

namespace debtTrackingApp.Controllers
{
    // Data Load, Data Update, Data Extraction Functionalities
    public class DataController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
