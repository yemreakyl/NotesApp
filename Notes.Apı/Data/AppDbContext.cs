using Microsoft.EntityFrameworkCore;
using Notes.Apı.Models.Entities;

namespace Notes.Apı.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Note> Notes { get; set; }
    }
}
