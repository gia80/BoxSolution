using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using YlibkaBox.Api.Dal.DbContexts;
using YlibkaBox.Api.Domain.Configurations;
using YlibkaBox.Api.Domain.Contracts;
using YlibkaBox.Api.Domain.Entities;

namespace YlibkaBox.Api.Dal.Repositories;

public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(BoxDbContext context, IOptions<DatabaseConfiguration> configuration)
        : base(context, configuration)
    {
    }

    public async Task<User> GetUser(string username)
    {
        return await Context.Users
            .SingleOrDefaultAsync(x => x.Username == username);
    }
}