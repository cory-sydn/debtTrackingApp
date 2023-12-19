using Microsoft.AspNetCore.Mvc;

namespace debtTrackingApp.Controllers
{
    public class CallScreenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
