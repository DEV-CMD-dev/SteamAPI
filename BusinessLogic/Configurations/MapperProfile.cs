using BusinessLogic.DTOs.Achievement;
using BusinessLogic.DTOs.Game;
using BusinessLogic.DTOs.Review;
using BusinessLogic.DTOs.Tag;
using DataAccess.Data.Entities;
using DataAccess.Data.Entities.DataAccess.Data.Entities;

namespace BusinessLogic.Configurations
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
            // Game mappings
            CreateMap<Game, GameDto>()
                .ForMember(dest => dest.HasRating, opt => opt.MapFrom(src => src.TotalReviews >= 10))
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
        }
    }
}