using BusinessLogic.DTOs.Game;

namespace BusinessLogic.Interfaces
{
    public interface IGameService
    {
        Task<IList<GameDto>> GetAll();
        Task<GameDto> Get(int id);
        Task<GameDto> Create(CreateGameDto model);
        Task Update(UpdateGameDto model);
        Task Delete(DeleteGameDto dto);
    }
}
