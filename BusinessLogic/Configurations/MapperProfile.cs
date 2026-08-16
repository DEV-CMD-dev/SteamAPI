using BusinessLogic.DTOs.Achievement;
using BusinessLogic.DTOs.Game;
using BusinessLogic.DTOs.GameVersion;
using BusinessLogic.DTOs.Screenshot;
using BusinessLogic.DTOs.Profile;
using BusinessLogic.DTOs.Tag;
using DataAccess.Data.Entities;

namespace BusinessLogic.Configurations
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
            // Profile mappings
            CreateMap<Profile, ProfileDto>().ReverseMap();
            CreateMap<Profile, PutProfileDto>().ReverseMap();
            CreateMap<PatchProfileDto, Profile>();

            // Game mappings
            CreateMap<Game, GameDto>()
                .ForMember(
                    dest => dest.TagIds,
                    opt => opt.MapFrom(src => src.Tags.Select(t => t.Id))
                );
            CreateMap<CreateGameDto, Game>()
                .ForMember(dest => dest.CoverImageHorizontal, opt => opt.Ignore())
                .ForMember(dest => dest.CoverImageVertical, opt => opt.Ignore());

            CreateMap<PutGameDto, Game>()
                .ForMember(dest => dest.CoverImageHorizontal, opt => opt.Ignore())
                .ForMember(dest => dest.CoverImageVertical, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.Ignore());

            // Tag mappings
            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<CreateTagDto, Tag>();
            CreateMap<PutTagDto, Tag>();

            // Achievement mappings
            CreateMap<Achievement, AchievementDto>().ReverseMap();
            CreateMap<CreateAchievementDto, Achievement>();
            CreateMap<PutAchievementDto, Achievement>();
            CreateMap<PatchAchievementDto, Achievement>();

            // Screenshot mappings
            CreateMap<Screenshot, ScreenshotDto>().ReverseMap();
            CreateMap<CreateScreenshotDto, Screenshot>();
            CreateMap<UpdateScreenshotDto, Screenshot>();

            // GameVersion mappings
            CreateMap<GameVersion, GameVersionDto>().ReverseMap();
            CreateMap<CreateGameVersionDto, GameVersion>();
            CreateMap<PutGameVersionDto, GameVersion>();
            CreateMap<PatchGameVersionDto, GameVersion>();
        }
    }
}


