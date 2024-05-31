using AutoMapper;
using BLL.DTOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL.MappingProfiles
{
    internal class MappingFrofile:Profile
    {
        public MappingFrofile() {
            CreateMap<Product, ProductDTO>().ReverseMap();

            CreateMap<Image, ImageDTO>().ReverseMap();
            CreateMap<Feedback, FeedbackDTO>().ReverseMap();
            CreateMap<Order, OrderRespDTO>().ReverseMap();
        }

    }
}
