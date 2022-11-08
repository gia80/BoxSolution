using Microsoft.EntityFrameworkCore;
using YlibkaBox.Api.Dal.EntityConfigurations;
using YlibkaBox.Api.Dal.Initializers;
using YlibkaBox.Api.Domain.Entities;

namespace YlibkaBox.Api.Dal.DbContexts;

public sealed class BoxDbContext : DbContext
{
    public BoxDbContext(DbContextOptions options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Box> Boxes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new BoxConfiguration());

        modelBuilder.Seed();
    }
}