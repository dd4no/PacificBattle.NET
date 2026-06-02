using Microsoft.EntityFrameworkCore;
using PacificBattle.Data.ContextModels;

namespace PacificBattle.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Ship> Ships { get; set; }
    }
}
