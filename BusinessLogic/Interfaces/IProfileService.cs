using BusinessLogic.DTOs.Profile;
using BusinessLogic.Helpers;

namespace BusinessLogic.Interfaces
{
    public interface IProfileService
    {
        Task<PaginatedList<ProfileDto>> GetAll(int pageNumber, int pageSize);
        Task<ProfileDto> GetById(int id);
        Task Patch(int id, string userId, PatchProfileDto dto);
        Task Put(int id, string userId, PutProfileDto dto); 
    }
}
