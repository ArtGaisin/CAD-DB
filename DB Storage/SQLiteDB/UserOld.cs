using Microsoft.EntityFrameworkCore;

namespace SQLiteDB;
/*
public class UserOld
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
}

public class ApplicationContextOld : DbContext
{
    public DbSet<UserOld> Users => Set<UserOld>();
    public ApplicationContextOld()
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=MainDB.db");
    }
}
*/