using Microsoft.EntityFrameworkCore;
using FilmLogAPI.Models;

namespace FilmLogAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<WatchlistItem> WatchlistItems { get; set; }
        public DbSet<WatchedItem> WatchedItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WatchlistItem>()
                .HasOne(w => w.User)
                .WithMany(u => u.Watchlist)
                .HasForeignKey(w => w.UserId);

            modelBuilder.Entity<WatchedItem>()
                .HasOne(w => w.User)
                .WithMany(u => u.WatchedList)
                .HasForeignKey(w => w.UserId);
        }
    }
}