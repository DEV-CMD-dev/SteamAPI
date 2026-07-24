using BusinessLogic.DTOs.Tag;

namespace BusinessLogic.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<TagDto>> GetAll();
        Task<TagDto> GetById(int id);
        Task Create(CreateTagDto dto);
        Task Patch(int id, PatchTagDto dto);
        Task Put(int id, PutTagDto dto);
        Task Delete(int id);
    }
}