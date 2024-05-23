using AutoMapper;
using BLL.DTOs;
using Common.Req.UserRequest;
using Common.Rsp;
using DAL.Models;

namespace BLL.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UpdateUserReq, User>()
                .ForMember(dest => dest.Avatar, opt => opt.Ignore());
            CreateMap<User ,UpdateUserRsp>();
        }
    }
}
