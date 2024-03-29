using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class NoteContext: DbContext
    {
        public NoteContext(DbContextOptions options) : base(options) { }

        public DbSet<Note> Notes { get; set; } 

    }
}
