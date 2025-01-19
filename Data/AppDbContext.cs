using Microsoft.EntityFrameworkCore;
using PacificBattle.Models;

namespace PacificBattle.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Side> Sides { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source = pacificbattle.db");
            }
        }
    }
}
