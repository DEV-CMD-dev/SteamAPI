
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
            CreateMap<PatchGameDto, Game>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                {
                    if (srcMember == null)
                        return false;
                    if (srcMember is DateTime dt && dt == default)
                        return false;

                    return true;
                }));

            // Tag mappings
            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<CreateTagDto, Tag>();
            CreateMap<PutTagDto, Tag>();
            CreateMap<PatchTagDto, Tag>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                {
                    if (srcMember == null)
                        return false;

                    return true;
                }));
        }
    }
}