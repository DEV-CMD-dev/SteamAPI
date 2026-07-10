using BusinessLogic.DTOs.Game;

namespace BusinessLogic.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<GameDto>> GetAll();
        Task<GameDto> GetById(int id);
        Task<GameDto> Create(string developerId, CreateGameDto dto);
        Task<GameDto> Update(int id, UpdateGameDto model);
        Task Delete(int gameId, string userId);
    }
}
