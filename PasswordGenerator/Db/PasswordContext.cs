using Microsoft.EntityFrameworkCore;
using PasswordGenerator.Models;

namespace PasswordGenerator.Db;

public class PasswordContext : DbContext
{
    public DbSet<PasswordModel> PasswordModels { get; set; }

    public PasswordContext()
    {
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=Passwords.db");
    }
}