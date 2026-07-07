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
    public class WatchedController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WatchedController(ApplicationDbContext context)
        {
            _context = context;
        }

        private int GetUserId() =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public IActionResult GetWatched()
        {
            var items = _context.WatchedItems
                .Where(w => w.UserId == GetUserId())
                .ToList();
            return Ok(items);
        }

        [HttpPost]
        public IActionResult AddToWatched(WatchedItem item)
        {
            item.UserId = GetUserId();
            item.TimesWatched = 1;
            _context.WatchedItems.Add(item);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetWatched), item);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateWatched(int id)
        {
            var item = _context.WatchedItems
                .FirstOrDefault(w => w.Id == id && w.UserId == GetUserId());
            if (item == null) return NotFound();
            item.TimesWatched++;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveFromWatched(int id)
        {
            var item = _context.WatchedItems
                .FirstOrDefault(w => w.Id == id && w.UserId == GetUserId());
            if (item == null) return NotFound();
            _context.WatchedItems.Remove(item);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPost("reset/{id}")]
        public IActionResult ResetTimesWatched(int id)
        {
            var item = _context.WatchedItems
                .FirstOrDefault(w => w.Id == id && w.UserId == GetUserId());
            if (item == null) return NotFound();
            item.TimesWatched = 1;
            _context.SaveChanges();
            return NoContent();
        }
    }
}