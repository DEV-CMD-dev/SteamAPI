using AutoMapper;
using DataAccess.Data.Entities;
using BusinessLogic.DTOs.Game;
using BusinessLogic.DTOs.Achievement;
using BusinessLogic.DTOs.Tag;

namespace BusinessLogic.Configurations
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<GameDto, Game>().ReverseMap();
            CreateMap<CreateGameDto, Game>().ReverseMap();
            CreateMap<CreateGameDto, GameDto>().ReverseMap();

            CreateMap<AchievementDto, Achievement>().ReverseMap();
            CreateMap<CreateAchivementDto, Achievement>().ReverseMap();
            CreateMap<CreateAchivementDto, AchievementDto>().ReverseMap();

            CreateMap<TagDto, Tag>().ReverseMap();
            CreateMap<CreateTagDto, Tag>().ReverseMap();
            CreateMap<CreateTagDto, TagDto>().ReverseMap();
        }
    }
}
