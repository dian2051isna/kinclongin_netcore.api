using Microsoft.AspNetCore.Mvc;

namespace KinclongIN.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
