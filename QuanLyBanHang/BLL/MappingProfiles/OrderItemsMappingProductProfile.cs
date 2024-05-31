using AutoMapper;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.MappingProfiles
{
    public class OrderItemsMappingProductProfile : Profile
    {
        public OrderItemsMappingProductProfile() {
            CreateMap<Orderitem,Product>().ReverseMap();
        }
    }
}
