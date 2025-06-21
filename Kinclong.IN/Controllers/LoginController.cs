using Microsoft.AspNetCore.Mvc;
using KinclongIN.Models;
using KinclongIN.DTO;

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
        [HttpPost("api/Register")]
        public IActionResult RegisterUser([FromBody] RegisterDTO registerDto)
        {
            if (registerDto == null || string.IsNullOrEmpty(registerDto.Email) || string.IsNullOrEmpty(registerDto.Password))
            {
                return BadRequest("Invalid user data.");
            }

            LoginContext loginContext = new LoginContext(__constr);
            bool isRegistered = loginContext.Register(registerDto, __config);
            if (isRegistered)
            {
                return Ok("Registration successful.");
            }
            else
            {
                return Conflict("User already exists or registration failed.");
            }
        }
    }
}
