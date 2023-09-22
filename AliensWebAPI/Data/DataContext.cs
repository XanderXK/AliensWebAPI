using AliensWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AliensWebAPI.Data;

public class DataContext : DbContext
{
    public DbSet<Alien> Aliens { get; set; }
    public DbSet<AlienSolarSystem> AliensSolarSystems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<SolarSystem> SolarSystems { get; set; }
    public DbSet<Ufologist> Ufologists { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AlienSolarSystem>().HasKey(aSystem => new
        {
            aSystem.AlienId,
            aSystem.SolarSystemId
        });

      //  modelBuilder.Entity<Category>().HasMany(c => c.Aliens).WithOne(a => a.Category);

        modelBuilder.Entity<AlienSolarSystem>().HasOne(a => a.Alien)
            .WithMany(s => s.SolarSystems).HasForeignKey(s => s.AlienId);

        modelBuilder.Entity<AlienSolarSystem>().HasOne(s => s.SolarSystem)
            .WithMany(a => a.AlienSolarSystems).HasForeignKey(a => a.SolarSystemId);
    }
}