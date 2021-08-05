using Microsoft.EntityFrameworkCore;

namespace NotesWebApp.Models
{
    public class NotesDataContext : DbContext
    {
        public DbSet<Note> Notices { get; set; }

        public NotesDataContext(DbContextOptions<NotesDataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
