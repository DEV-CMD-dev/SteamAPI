using BusinessLogic.DTOs.Game;

namespace BusinessLogic.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<GameDto>> GetAll(int? pageNumber, int? pageSize);
        Task<GameDto> GetById(int id);
        Task<GameDto> Create(string developerId, CreateGameDto dto);
        Task Patch(int id, PatchGameDto model);
        Task Put(int id, PutGameDto model);
        Task Delete(int gameId, string userId);
    }
}