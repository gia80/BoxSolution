using Microsoft.EntityFrameworkCore;
using YlibkaBox.Api.Domain.Entities;
using YlibkaBox.Api.Domain.Enums;
using YlibkaBox.Api.Infrastructure.Providers;

namespace YlibkaBox.Api.Dal.Initializers;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        var passwordSalt = CredentialsProvider.GenerateCredentials("p12345678");

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Username = "l12345678",
                FirstName = "James",
                LastName = "Bond",
                UserStatus = UserStatus.Active,
                Password = passwordSalt.Item1,
                Salt = passwordSalt.Item2
            }
        );

        var boxes = new List<Box>();

        for (var i = 1000; i < 1200; i++) boxes.Add(new Box { Id = i, Specification = $"Box {i}" });

        modelBuilder.Entity<Box>().HasData(boxes);
    }
}