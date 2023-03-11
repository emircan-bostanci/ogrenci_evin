using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ogrencievin.Models.Entities;
using ogrencievin.Models.GeoEntity;

namespace ogrencievin.Models;

public class Context : IdentityDbContext<User>{
    public Context(DbContextOptions<Context> options):base(options) {}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost:5432;Username=postgres;Password=root;Database=ogrencievin;");
        base.OnConfiguring(optionsBuilder);
        
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Estate>().HasOne(u => u.author).WithMany(u=>u.sharedEstates);
        builder.Entity<Estate>().HasMany(u => u.likers).WithMany(u=>u.favoriteEstates);

        builder.Entity<Message>().HasOne(m => m.sender).WithMany(u => u.sendedMessages);
        builder.Entity<Message>().HasOne(m => m.receiver).WithMany(u => u.receivedMessages);
        base.OnModelCreating(builder);
    }
    public DbSet<Estate> estates{get;set;}
}