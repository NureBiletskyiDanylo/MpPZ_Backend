using KidSMedia_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KidSMedia_API.Controllers
{
    public class AccountController(IUserRepository repository, ITokenService tokenService) : BaseApiController
    {
    }
}
