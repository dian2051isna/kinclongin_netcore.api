using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using KinclongIN.Models;
using KinclongIN.DTO;
using Kinclong.IN.Models;


namespace KinclongIN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : Controller
    {
        private readonly BookingContext _context;

        public BookingController(BookingContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Bookings>> ListBooking()
        {
            var list = _context.ListBookings();

            var dtoList = list.Select(b => new BookingResponseDTO
            {
                id_booking = b.id_booking,
                booking_date = b.booking_date,
                booking_status = b.booking_status,
                idUser = b.IdUser
            }).ToList();

            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public ActionResult<Bookings> GetBookingById(int id)
        {
            var booking = _context.GetBookingById(id);

            if (booking == null)
            {
                return NotFound($"Booking dengan ID {id} tidak ditemukan.");
            }

            return Ok(booking);
        }

        [HttpPost]
        public IActionResult AddBooking([FromBody] BookingCreateDTO dto)
        {
            if (dto == null)
                return BadRequest("Data booking tidak valid.");

            var newBooking = new Bookings
            {
                booking_date = dto.booking_date,
                booking_status = dto.booking_status,
                IdUser = dto.idUser,
                created_at = DateTime.Now,
                updated_at = DateTime.Now
            };

            bool result = _context.AddBooking(newBooking);

            if (result)
                return Ok("Booking berhasil ditambahkan.");
            else
                return StatusCode(500, "Terjadi kesalahan saat menambahkan booking.");
        }

        [HttpPut("{id}/status")]
        public IActionResult UpdateBookingStatus(int id, [FromBody] string status)
        {
            bool result = _context.UpdateBookingStatus(id, status);

            if (result)
                return Ok($"Status booking ID {id} berhasil diperbarui menjadi '{status}'.");
            else
                return NotFound($"Booking dengan ID {id} tidak ditemukan.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            bool result = _context.DeleteBooking(id);

            if (result)
                return Ok($"Booking ID {id} berhasil dihapus.");
            else
                return NotFound($"Booking dengan ID {id} tidak ditemukan.");
        }
    }
}
