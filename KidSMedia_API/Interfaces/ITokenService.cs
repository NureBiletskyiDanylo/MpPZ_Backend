using KidSMedia_API.Data.Entities;

namespace KidSMedia_API.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}
