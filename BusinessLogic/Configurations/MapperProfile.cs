using DataAccess.Data.Entities;
using BusinessLogic.DTOs.Game;
using BusinessLogic.DTOs.Tag;


namespace BusinessLogic.Configurations
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
            // Game mappings
            CreateMap<GameDto, Game>().ReverseMap();
            CreateMap<CreateGameDto, Game>().ReverseMap();
            CreateMap<CreateGameDto, GameDto>().ReverseMap();

            CreateMap<PatchGameDto, GameDto>().ReverseMap();
            CreateMap<PatchGameDto, Game>()
             .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
             {
                 if (srcMember == null) 
                     return false;
                 if (srcMember is DateTime dateTime && dateTime == default(DateTime)) 
                     return false;

                 return true;
             }));

            CreateMap<PutGameDto, GameDto>().ReverseMap();
            CreateMap<PutGameDto, Game>().ReverseMap();

            // Tag mappings
            CreateMap<TagDto, Tag>().ReverseMap();
            CreateMap<CreateTagDto, Tag>().ReverseMap();
            CreateMap<UpdateTagDto, Tag>();
        }
    }
}