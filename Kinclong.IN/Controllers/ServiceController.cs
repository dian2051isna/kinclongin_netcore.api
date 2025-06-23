using Microsoft.AspNetCore.Mvc;
using KinclongIN.Models;
using KinclongIN.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace KinclongIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        //  GET: /api/services
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllServices()
        {
            var services = _context.Services.ToList();
            return Ok(services);
        }

        //  GET: /api/services/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetServiceById(int id)
        {
            var service = _context.Services.Find(id);
            if (service == null)
                return NotFound("Jasa tidak ditemukan");

            return Ok(service);
        }

        //  POST: /api/services
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult CreateService([FromBody] ServiceDTO dto)
        {
            var service = new Services
            {
                service_name = dto.ServiceName,
                service_category = dto.ServiceCategory,
                service_price = dto.ServicePrice,
                service_photo = dto.ServicePhoto,
                created_at = DateTime.Now,
                updated_at = DateTime.Now
            };

            _context.Services.Add(service);
            _context.SaveChanges();

            return Ok(new { message = "Jasa berhasil ditambahkan", service });
        }

        //  PUT: /api/services/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateService(int id, [FromBody] ServiceDTO dto)
        {
            var service = _context.Services.Find(id);
            if (service == null)
                return NotFound("Jasa tidak ditemukan");

            service.ServiceName = dto.ServiceName;
            service.ServiceCategory = dto.ServiceCategory;
            service.ServicePrice = dto.ServicePrice;
            service.ServicePhoto = dto.ServicePhoto;
            service.UpdatedAt = DateTime.Now;

            _context.SaveChanges();

            return Ok(new { message = "Jasa berhasil diupdate", service });
        }

        //  DELETE: /api/services/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteService(int id)
        {
            var service = _context.Services.Find(id);
            if (service == null)
                return NotFound("Jasa tidak ditemukan");

            _context.Services.Remove(service);
            _context.SaveChanges();

            return Ok(new { message = "Jasa berhasil dihapus" });
        }
    }
}
