using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FilmLogAPI.Data;
using FilmLogAPI.Models;

namespace FilmLogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WatchlistController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WatchlistController(ApplicationDbContext context)
        {
            _context = context;
        }

        private int GetUserId() =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public IActionResult GetWatchlist()
        {
            var items = _context.WatchlistItems
                .Where(w => w.UserId == GetUserId())
                .ToList();
            return Ok(items);
        }

        [HttpPost]
        public IActionResult AddToWatchlist(WatchlistItem item)
        {
            item.UserId = GetUserId();
            _context.WatchlistItems.Add(item);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetWatchlist), item);
        }

        [HttpDelete("{title}")]
        public IActionResult RemoveFromWatchlist(string title)
        {
            var item = _context.WatchlistItems
                .FirstOrDefault(w => w.Title == title && w.UserId == GetUserId());
            if (item == null) return NotFound();
            _context.WatchlistItems.Remove(item);
            _context.SaveChanges();
            return NoContent();
        }
    }
}