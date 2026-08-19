using BusinessLogic.DTOs.Achievement;
using BusinessLogic.DTOs.Game;
using BusinessLogic.DTOs.GameVersion;
using BusinessLogic.DTOs.Profile;
using BusinessLogic.DTOs.Review;
using BusinessLogic.DTOs.Screenshot;
using BusinessLogic.DTOs.Tag;
using BusinessLogic.Helpers;
using DataAccess.Data.Entities;
using DataAccess.Data.Entities.DataAccess.Data.Entities;

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
                .ForMember(dest => dest.HasRating, opt => opt.MapFrom(src => GameUtils.GetHasRating(src.TotalReviews)))
                .ReverseMap();
            CreateMap<CreateGameDto, Game>();
            CreateMap<PutGameDto, Game>();
            CreateMap<PatchGameDto, Game>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                {
                    if (srcMember == null)
                        return false;
                    if (srcMember is DateTime dt && dt == default)
                        return false;

                    return true;
                }));
                
            CreateMap<Game, GameDto>()
                .ForMember(
                    dest => dest.TagIds,
                    opt => opt.MapFrom(src => src.Tags.Select(t => t.Id))
                );
            CreateMap<PutGameDto, Game>()
                .ForMember(dest => dest.Tags, opt => opt.Ignore());


            // Tag mappings
            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<CreateTagDto, Tag>();
            CreateMap<PutTagDto, Tag>();


            // Review mappings
            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => src.User!.UserName));

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


