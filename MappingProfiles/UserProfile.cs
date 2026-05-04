using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Inventra.DTOs.Auth;

namespace Inventra.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<IdentityUser, UserDto>();
        CreateMap<RegisterDto, IdentityUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
    }
}