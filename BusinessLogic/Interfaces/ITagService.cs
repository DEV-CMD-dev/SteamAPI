using BusinessLogic.DTOs.Tag;

namespace BusinessLogic.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<TagDto>> GetAll();
        Task<TagDto> GetById(int id);
        Task Create(CreateTagDto dto);
        Task Update(int id ,UpdateTagDto dto);
        Task Delete(int id);
    }
}
