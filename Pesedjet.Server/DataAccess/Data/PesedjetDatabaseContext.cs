using Microsoft.EntityFrameworkCore;
using Pesedjet.Server.Models;

namespace Pesedjet.Server.DataAccess.Data;

public class PesedjetDatabaseContext : DbContext
{
    public PesedjetDatabaseContext(DbContextOptions<PesedjetDatabaseContext> options) : base(options) { }

    public DbSet<Player> Players { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<TwoFactorAuthenticator> TwoFactorAuthenticators { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Player>().ToTable("PLAYER");
        modelBuilder.Entity<Profile>().ToTable("PROFILE");
        modelBuilder.Entity<TwoFactorAuthenticator>().ToTable("TWO_FACTOR_AUTHENTICATOR");
        
        //TO DO: lambdas sí,pero descriptivos: agregar a estandar 
        modelBuilder.Entity<Player>().HasIndex(p => p.Username).IsUnique();
        
        modelBuilder.Entity<Player>().HasIndex(p => p.Email).IsUnique();
        
        modelBuilder.Entity<Player>()
            .Property(p => p.RegisterDate)
            .HasDefaultValueSql("GETDATE()");
        
        modelBuilder.Entity<Profile>()
            .HasOne(p => p.Player)
            .WithOne(pl => pl.Profile)
            .HasForeignKey<Profile>(p => p.PlayerId);
    }
}