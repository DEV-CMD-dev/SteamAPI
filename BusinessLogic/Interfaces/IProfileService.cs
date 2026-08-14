using BusinessLogic.DTOs.Profile;
using BusinessLogic.Helpers;

namespace BusinessLogic.Interfaces
{
    public interface IProfileService
    {
        Task<PaginatedList<ProfileDto>> GetAll(int pageNumber, int pageSize);
        Task<ProfileDto> GetById(string userId);
        Task Patch(string userId, PatchProfileDto dto);
        Task Put(string userId, PutProfileDto dto); 
    }
}
