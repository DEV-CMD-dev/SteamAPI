using AutoMapper;
using DataAccess.Data.Entities;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shared.Configurations
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<GameDto, Game>().ReverseMap();
        }
    }
}
