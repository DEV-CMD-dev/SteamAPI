using BusinessLogic.DTOs.Achievement;
using BusinessLogic.DTOs.Game;
using BusinessLogic.DTOs.GameVersion;
using BusinessLogic.DTOs.Profile;
using BusinessLogic.DTOs.Review;
using BusinessLogic.DTOs.Screenshot;
using BusinessLogic.DTOs.Item;
using BusinessLogic.DTOs.Tag;
using BusinessLogic.Helpers;
using DataAccess.Data.Entities;
using DataAccess.Data.Entities.DataAccess.Data.Entities;
using BusinessLogic.DTOs.InventoryItem;
using BusinessLogic.DTOs.TradeOffer;

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
            CreateMap<MiniProfileDto, Profile>();


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
                
            CreateMap<Game, GameDto>()
                .ForMember(
                    dest => dest.DeveloperName,
                    opt => opt.MapFrom(src => src.Developer != null ? src.Developer.UserName : string.Empty)
                )
                .ForMember(dest => dest.Screenshots, opt => opt.ExplicitExpansion())
                .ForMember(dest => dest.TagIds, opt => opt.MapFrom(src => src.Tags.Select(t => t.Id)));
            CreateMap<PutGameDto, Game>()
                .ForMember(dest => dest.Tags, opt => opt.Ignore());


            // Tag mappings
            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<CreateTagDto, Tag>();
            CreateMap<PutTagDto, Tag>();


            // Review mappings
            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => src.User!.UserName))
                .ForMember(dest => dest.UserAvatarUrl,
                    opt => opt.MapFrom(src => src.User!.Profile != null ? src.User.Profile.Avatar : null))
                .ForMember(dest => dest.UserGamesOwnedCount,
                    opt => opt.MapFrom(src => src.User!.UserGames.Count))
                .ForMember(dest => dest.UserReviewsCount,
                    opt => opt.MapFrom(src => src.User!.Reviews.Count));

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

            // Item mappings
            CreateMap<Item, ItemDto>()
                .ForMember(dest => dest.GameTitle,
                    opt => opt.MapFrom(src => src.Game != null ? src.Game.Title : string.Empty))
                .ForMember(dest => dest.GameIconUrl,
                    opt => opt.MapFrom(src => src.Game != null
                        ? (!string.IsNullOrEmpty(src.Game.IconUrl) ? src.Game.IconUrl : src.Game.CoverImageHorizontal)
                        : null));
            CreateMap<ItemDto, Item>();
            CreateMap<CreateItemDto, Item>();
            CreateMap<PutItemDto, Item>();
            CreateMap<PatchItemDto, Item>();

            // InventoryItem mappings
            CreateMap<InventoryItem, InventoryItemDto>().ReverseMap();
            CreateMap<CreateInventoryItemDto, InventoryItem>();
            CreateMap<PutInventoryItemDto, InventoryItem>();
            CreateMap<PatchInventoryItemDto, InventoryItem>();

            //TradeOffer mappings
            CreateMap<InventoryItem, TradeOfferItemDto>()
                .ForMember(dest => dest.InventoryItemId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Item.Name))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Item.ImageUrl))
                .ForMember(dest => dest.GameTitle, opt => opt.MapFrom(src => src.Item.Game.Title));

            CreateMap<TradeOffer, TradeOfferDto>()
                .ForMember(dest => dest.SenderName,
                    opt => opt.MapFrom(src => src.Sender != null ? src.Sender.UserName : string.Empty))
                .ForMember(dest => dest.ReceiverName,
                    opt => opt.MapFrom(src => src.Receiver != null ? src.Receiver.UserName : string.Empty))
                .ForMember(dest => dest.SenderItem,
                    opt => opt.MapFrom(src => src.SenderInventoryItem))
                .ForMember(dest => dest.ReceiverItem,
                    opt => opt.MapFrom(src => src.ReceiverInventoryItem));

        }
    }
}


