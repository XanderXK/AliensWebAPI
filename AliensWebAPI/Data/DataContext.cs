using AliensWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AliensWebAPI.Data;

public class DataContext : DbContext
{
    public DbSet<Alien> Aliens { get; set; } = null!;
    public DbSet<AlienSolarSystem> AliensSolarSystems { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<SolarSystem> SolarSystems { get; set; } = null!;
    public DbSet<Ufologist> Ufologists { get; set; } = null!;

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


        modelBuilder.Entity<AlienSolarSystem>()
            .HasOne(a => a.Alien)
            .WithMany(s => s.SolarSystems)
            .HasForeignKey(s => s.AlienId);

        modelBuilder.Entity<AlienSolarSystem>()
            .HasOne(s => s.SolarSystem)
            .WithMany(a => a.AlienSolarSystems)
            .HasForeignKey(a => a.SolarSystemId);
    }
}