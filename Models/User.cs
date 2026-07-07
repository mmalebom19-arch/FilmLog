using System.ComponentModel.DataAnnotations;

namespace FilmLogAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<WatchlistItem>? Watchlist { get; set; }
        public List<WatchedItem>? WatchedList { get; set; }
    }
}