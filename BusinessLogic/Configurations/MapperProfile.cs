
using BusinessLogic.DTOs.Achievement;
using BusinessLogic.DTOs.Game;
using BusinessLogic.DTOs.GameVersion;
using BusinessLogic.DTOs.Screenshot;
using BusinessLogic.DTOs.Tag;
using BusinessLogic.Extensions;
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
            CreateMap<PatchGameDto, Game>().IgnoreNull();
               
            // Tag mappings
            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<CreateTagDto, Tag>();
            CreateMap<PutTagDto, Tag>();
            CreateMap<PatchTagDto, Tag>().IgnoreNull();

            // Tag mappings
            CreateMap<Achievement, AchievementDto>().ReverseMap();
            CreateMap<CreateAchievementDto, Achievement>();
            CreateMap<PutAchievementDto, Achievement>();
            CreateMap<PatchAchievementDto, Achievement>().IgnoreNull();

            // Screenshot mappings
            CreateMap<Screenshot, ScreenshotDto>().ReverseMap();
            CreateMap<CreateScreenshotDto, Screenshot>();
            CreateMap<PutScreenshotDto, Screenshot>();
            CreateMap<PatchScreenshotDto, Screenshot>().IgnoreNull();

            // GameVersion mappings
            CreateMap<GameVersion, GameVersionDto>().ReverseMap();
            CreateMap<CreateGameVersionDto, GameVersion>();
            CreateMap<PutGameVersionDto, GameVersion>();
            CreateMap<PatchGameVersionDto, GameVersion>().IgnoreNull();
        }
    }
}