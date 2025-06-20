using Microsoft.AspNetCore.Mvc;
using KinclongIN.Models;

namespace KinclongIN.Controllers
{
    public class LoginController : Controller
    {
        private string __constr;
        private IConfiguration __config;

        public LoginController(IConfiguration config)
        {
            __config = config;
            __constr = __config.GetConnectionString("WebApiDatabase");
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("api/Login")]
        public IEnumerable<Login> LoginUser(string namaUser, string password)
        {
            LoginContext login = new LoginContext(__constr);
            List<Login> listlogin = login.Autentifikasi(namaUser, password, __config);
            return listlogin.ToArray();
        }
    }
}
