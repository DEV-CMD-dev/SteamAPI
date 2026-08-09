using BusinessLogic.Helpers;
using BusinessLogic.DTOs.Game;
using BusinessLogic.Extensions.SearchFilters;

namespace BusinessLogic.Interfaces
{
    public interface IGameService
    {
        Task<PaginatedList<GameDto>> GetAll(int pageNumber, int pageSize, GameParameters gameParams);
        Task<GameDto> GetById(int id);
        Task<GameDto> Create(string userId, CreateGameDto dto);
        Task Patch(int id, string userId, PatchGameDto model);
        Task Put(int id, string userId, PutGameDto model);
        Task Delete(int gameId, string userId);
    }
}