using AutoMapper;
using KidSMedia_API.Data.Entities;
using KidSMedia_API.DTOs;

namespace KidSMedia_API.Helpers;

public class AutoMapperProfiles : Profile
{

    public AutoMapperProfiles()
    {
        CreateMap<AccountDto, User>();
        CreateMap<User, AccountDto>();
    }
}
