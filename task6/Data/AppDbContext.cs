using Microsoft.EntityFrameworkCore;
using task6.Models;

namespace task6.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Drawing> Drawings { get; set; }
    public DbSet<Table> Tables { get; set; }
}