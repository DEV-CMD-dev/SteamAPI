using BusinessLogic.DTOs.Profile;

namespace BusinessLogic.Interfaces
{
    public interface IProfileService
    {
        Task<IEnumerable<ProfileDto>> GetAll();
        Task<ProfileDto> GetById(int id);
        Task Create(ProfileDto dto);
        Task Update(int id, ProfileDto dto);
        Task Delete(int id);
    }
}