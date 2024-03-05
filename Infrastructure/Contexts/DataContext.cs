using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Contexts;

public partial class DataContext : IdentityDbContext<ApplicationUser>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<ProfilePictureEntity> ProfilePictures { get; set; }
    public DbSet<UserProfileEntity> UserProfiles { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddFilter((category, level) => level >= LogLevel.Warning)));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        //Unique indexes
        modelBuilder.Entity<AddressEntity>()
            .HasIndex(x => new { x.AddressLine1, x.AddressLine2, x.PostalCode, x.City })
            .IsUnique();


        modelBuilder.Entity<UserProfileEntity>()
            .HasIndex(x => x.Email)
            .IsUnique();


        modelBuilder.Entity<ApplicationUser>()
            .HasOne(u => u.UserProfile)
            .WithOne(p => p.User)
            .HasForeignKey<UserProfileEntity>(p => p.Id);
    }
}