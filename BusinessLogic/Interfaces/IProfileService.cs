using BusinessLogic.DTOs.Profile;

namespace BusinessLogic.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileDto> GetById(string userId);
        Task<MiniProfileDto> GetMyProfile(string userId);
        Task Patch(string userId, PatchProfileDto dto);
        Task Put(string userId, PutProfileDto dto);
        Task<List<ProfileSearchResultDto>> SearchByUserName(string query);
        Task<List<ProfileSearchResultDto>> SearchFriendsByUserName(string query, string currentUserId);
    }
}
