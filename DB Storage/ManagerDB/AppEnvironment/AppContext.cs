using ManagerDB.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagerDB.AppEnvironment;

public class AppContext : DbContext
{
    public DbSet<Material> Materials { get; set; }
    public DbSet<Developer> Developers { get; set; }
    public DbSet<PartDocument> PartDocuments { get; set; }
    public DbSet<DrawingDocument> DrawingDocuments { get; set; }
    public DbSet<AssemblyDocument> AssemblyDocuments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }
}

