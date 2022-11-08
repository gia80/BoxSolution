using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using YlibkaBox.Api.Dal.DbContexts;
using YlibkaBox.Api.Domain.Configurations;
using YlibkaBox.Api.Domain.Contracts;
using YlibkaBox.Api.Domain.Entities;

namespace YlibkaBox.Api.Dal.Repositories;

public class BoxRepository : BaseRepository, IBoxRepository
{
    public BoxRepository(BoxDbContext context, IOptions<DatabaseConfiguration> configuration)
        : base(context, configuration)
    {
    }

    public async Task<Box> GetBox(int id)
    {
        return await Context.Boxes.FindAsync(id);
    }

    public async Task<List<Box>> GetBoxes()
    {
        return await Context.Boxes.ToListAsync();
    }
}