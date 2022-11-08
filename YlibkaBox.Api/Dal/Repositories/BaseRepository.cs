using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using YlibkaBox.Api.Dal.DbContexts;
using YlibkaBox.Api.Domain.Configurations;

namespace YlibkaBox.Api.Dal.Repositories;

public abstract class BaseRepository
{
    protected readonly BoxDbContext Context;

    protected BaseRepository(BoxDbContext context, IOptions<DatabaseConfiguration> configuration)
    {
        Context = context;
        Context.Database.SetCommandTimeout(configuration.Value.BaseTimeoutMilliseconds);
    }
}