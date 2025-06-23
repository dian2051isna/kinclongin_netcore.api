using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KinclongIN.Models;
using System.Security.Claims;
using KinclongIN.DTO;

namespace KinclongIN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewContext _context;

        public ReviewController(ReviewContext context)
        {
            _context = context;
        }

        // POST: /api/review
        [HttpPost]
        [Authorize]
        public IActionResult AddReview([FromBody] ReviewDTO reviewDTO)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var review = new Review
            {
                IdUser = userId,
                IdOrder = reviewDTO.IdOrder,
                Rating = reviewDTO.Rating,
                Comment = reviewDTO.Comment,
                CreatedAt = DateTime.UtcNow
            };

            var success = _context.AddReview(review);

            if (success)
                return Ok(new { message = "Review berhasil ditambahkan." });
            else
                return BadRequest("Gagal menambahkan review.");
        }


        // DELETE: /api/review/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteReview(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var success = _context.DeleteReview(id, userId);

            if (success)
                return Ok(new { message = "Review berhasil dihapus." });
            else
                return NotFound("Review tidak ditemukan atau bukan milik Anda.");
        }
    }
}
