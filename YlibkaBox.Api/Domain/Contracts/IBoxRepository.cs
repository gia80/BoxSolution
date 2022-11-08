using YlibkaBox.Api.Domain.Entities;

namespace YlibkaBox.Api.Domain.Contracts;

public interface IBoxRepository
{
    Task<Box> GetBox(int id);
    Task<List<Box>> GetBoxes();
}