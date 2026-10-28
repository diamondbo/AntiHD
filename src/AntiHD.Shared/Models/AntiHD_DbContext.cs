using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace AntiHD.Models;

public class AntiHD_DbContext : DbContext
{
    public AntiHD_DbContext(DbContextOptions<AntiHD_DbContext> options) : base(options){}
    public DbSet<Users> Users { get; set; }

    public DbSet<Roles> Roles { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Roles>().HasData(
            new Roles { Id = 1, Name = "Viewer" },
            new Roles { Id = 2, Name = "User" },
            new Roles { Id = 3, Name = "Admin" }
        );
        modelBuilder.Entity<Users>()
            .HasOne<Roles>()
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
public class AntiHD_DbContextFactory : IDesignTimeDbContextFactory<AntiHD_DbContext>
    {
        public AntiHD_DbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AntiHD_DbContext>();
            var dbPath = Path.GetFullPath(
            Path.Combine(Directory.GetCurrentDirectory(), "..", "AntiHD.Shared", "antihd.db"));
            // Hardcode your development/local connection string here safely for migrations
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
            

            return new AntiHD_DbContext(optionsBuilder.Options);
        }
    }
   