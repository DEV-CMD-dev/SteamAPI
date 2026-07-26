using BusinessLogic.Classes.Helpers;
using BusinessLogic.DTOs.Game;

namespace BusinessLogic.Interfaces
{
    public interface IGameService
    {
        Task<PaginatedList<GameDto>> GetAll(int pageNumber, int pageSize);
        Task<GameDto> GetById(int id);
        Task<GameDto> Create(string userId, CreateGameDto dto);
        Task Patch(int id, string userId, PatchGameDto model);
        Task Put(int id, string userId, PutGameDto model);
        Task Delete(int gameId, string userId);
    }
}