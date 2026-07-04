using AutoMapper;
using DataAccess.Data.Entities;
using BusinessLogic.DTOs.Game;


namespace BusinessLogic.Configurations
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<GameDto, Game>().ReverseMap();
            CreateMap<CreateGameDto, Game>().ReverseMap();
            CreateMap<CreateGameDto, GameDto>().ReverseMap();
            CreateMap<UpdateGameDto, GameDto>().ReverseMap();
            CreateMap<UpdateGameDto, Game>().ForAllMembers(opts => opts.Condition((src,dest,srcMember) =>
            {
                if (srcMember == null) return false;
                return true;
            }));


        }
    }
}
