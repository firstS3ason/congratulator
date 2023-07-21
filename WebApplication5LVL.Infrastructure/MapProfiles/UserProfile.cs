
using AutoMapper;
using WebApplication5LVL.Contracts.User;
using WebApplication5LVL.Domain.Models;

namespace WebApplication5LVL.Infrastructure.MapProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, InfoUserResponse>()
                .ReverseMap();

            CreateMap<User, CreateUserRequest>()
                .ReverseMap();

            CreateMap<User, UpdateUserRequest>()
                .ReverseMap();
        }
    }
}
