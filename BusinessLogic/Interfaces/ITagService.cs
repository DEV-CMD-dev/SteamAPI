using BusinessLogic.Helpers;
using BusinessLogic.DTOs.Tag;

namespace BusinessLogic.Interfaces
{
    public interface ITagService
    {
        Task<PaginatedList<TagDto>> GetAll(int pageNumber, int pageSize);
        Task<TagDto> GetById(int id);
        Task Create(string userId,CreateTagDto dto);
        Task Patch(string userId, int id, PatchTagDto dto);
        Task Put(string userId, int id, PutTagDto dto);
        Task Delete(string userId, int id);
    }
}