using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthServer.Core.DTOs;
using AuthServer.Core.Entities;
using AutoMapper;

namespace AuthServer.Service.Mappings
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<UserApp, UserAppDto>().ReverseMap();
        }
    }
}
