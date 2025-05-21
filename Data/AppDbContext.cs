using ekzVar.Models;
using Microsoft.EntityFrameworkCore;

namespace ekzVar.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Materials> Materials { get; set; }
        public DbSet<Oboi> Oboi { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source=bd.db");
        }
    }
}
