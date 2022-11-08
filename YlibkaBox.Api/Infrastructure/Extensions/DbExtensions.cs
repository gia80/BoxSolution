using Microsoft.EntityFrameworkCore;
using YlibkaBox.Api.Dal.DbContexts;
using YlibkaBox.Api.Domain.Configurations;

namespace YlibkaBox.Api.Infrastructure.Extensions;

public static class DbExtensions
{
    public static void AddDb(this IServiceCollection services, DatabaseConfiguration configuration)
    {
        services.AddDbContext<BoxDbContext>(options => options
            .UseSqlite($"Data Source={AppDomain.CurrentDomain.BaseDirectory}/{configuration.DbName}.db"));
    }
}