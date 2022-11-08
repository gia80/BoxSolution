using YlibkaBox.Api.Domain.Enums;

namespace YlibkaBox.Api.Domain.Entities;

public class User
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserStatus UserStatus { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
}