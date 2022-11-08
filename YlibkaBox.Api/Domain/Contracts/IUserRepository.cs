using YlibkaBox.Api.Domain.Entities;

namespace YlibkaBox.Api.Domain.Contracts;

public interface IUserRepository
{
    Task<User> GetUser(string username);
}