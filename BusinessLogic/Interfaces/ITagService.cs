using BusinessLogic.Classes.Helpers;
using BusinessLogic.DTOs.Tag;

namespace BusinessLogic.Interfaces
{
    public interface ITagService
    {
        Task<PaginatedList<TagDto>> GetAll(int pageNumber, int pageSize);
        Task<TagDto> GetById(int id);
        Task Create(CreateTagDto dto);
        Task Patch(int id, PatchTagDto dto);
        Task Put(int id, PutTagDto dto);
        Task Delete(int id);
    }
}