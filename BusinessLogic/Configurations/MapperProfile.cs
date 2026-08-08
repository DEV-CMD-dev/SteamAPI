using BusinessLogic.DTOs.Achievement;
using BusinessLogic.DTOs.Game;
using BusinessLogic.DTOs.Tag;
using DataAccess.Data.Entities;

namespace BusinessLogic.Configurations
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
            // Game mappings
            CreateMap<Game, GameDto>().ReverseMap();
            CreateMap<CreateGameDto, Game>();
            CreateMap<PutGameDto, Game>();

            // Tag mappings
            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<CreateTagDto, Tag>();
            CreateMap<PutTagDto, Tag>();

            // Achievement mappings
            CreateMap<Achievement, AchievementDto>().ReverseMap();
            CreateMap<CreateAchievementDto, Achievement>();
            CreateMap<PutAchievementDto, Achievement>();
            CreateMap<PatchAchievementDto, Achievement>();
        }
    }
}