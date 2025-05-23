using Microsoft.AspNetCore.Mvc;

namespace KinclongIN.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
