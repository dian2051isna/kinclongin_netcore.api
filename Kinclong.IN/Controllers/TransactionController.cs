using Microsoft.AspNetCore.Mvc;

namespace KinclongIN.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
