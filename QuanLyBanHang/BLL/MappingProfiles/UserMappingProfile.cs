using AutoMapper;
using BLL.DTOs;
using DAL.Models;

namespace BLL.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
