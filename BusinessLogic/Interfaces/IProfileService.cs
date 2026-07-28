using BusinessLogic.DTOs.Profile;

namespace BusinessLogic.Interfaces
{
    public interface IProfileService
    {
        Task<IEnumerable<ProfileDto>> GetAll();
        Task<ProfileDto> GetById(int id);
        Task Patch(int id, PatchProfileDto dto);
        Task Put(int id, PutProfileDto dto); 
    }
}
