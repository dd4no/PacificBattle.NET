﻿using Microsoft.EntityFrameworkCore;
using PacificBattle.Models;

namespace PacificBattle.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Side> Sides { get; set; }
    }
}
