using Microsoft.EntityFrameworkCore;
using learn_asp_clean_structure.Models;

namespace learn_asp_clean_structure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}