using AutoMapper;
using DataAccess.Data.Entities;
using BusinessLogic.DTOs;

namespace BusinessLogic.Configurations
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<GameDto, Game>().ReverseMap();
        }
    }
}
