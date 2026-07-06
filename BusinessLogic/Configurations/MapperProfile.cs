using AutoMapper;
using DataAccess.Data.Entities;
using BusinessLogic.DTOs.Game;
using BusinessLogic.DTOs.Tag;


namespace BusinessLogic.Configurations
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Game mappings
            CreateMap<GameDto, Game>().ReverseMap();
            CreateMap<CreateGameDto, Game>().ReverseMap();
            CreateMap<CreateGameDto, GameDto>().ReverseMap();
            CreateMap<UpdateGameDto, GameDto>().ReverseMap();
            CreateMap<UpdateGameDto, Game>().ForAllMembers(opts => opts.Condition((src,dest,srcMember) =>
            {
                if (srcMember == null) return false;
                return true;
            }));

            // Tag mappings
            CreateMap<TagDto, Tag>().ReverseMap();
            CreateMap<CreateTagDto, Tag>().ReverseMap();
            CreateMap<UpdateTagDto, Tag>();



        }
    }
}
