using Microsoft.AspNetCore.Mvc;
using KinclongIN.Models;
using KinclongIN.DTO;
using KinclongIN.Helpers;

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
        public IActionResult Login([FromBody] LoginDTO loginData)
        {
            var context = new LoginContext(__constr);
            var user = context.Autentifikasi(loginData.Email, loginData.Password, __config);

            if (user == null)
            {
                return Unauthorized(new { message = "Email atau password salah" });
            }

            var jwtHelper = new JwtHelper(__config);
            var token = jwtHelper.GenerateToken(user);

            return Ok(new
            {
                token,
                user = new
                {
                    id = user.id_person,
                    nama = user.nama,
                    email = user.email,
                    peran = user.nama_peran
                }
            });
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
