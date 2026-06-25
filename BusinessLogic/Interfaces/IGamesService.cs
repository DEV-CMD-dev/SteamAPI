using BusinessLogic.DTOs.Game;

namespace BusinessLogic.Interfaces
{
    public interface IGamesService
    {
        Task<IList<GameDto>> GetAll();
        Task<GameDto?> Get(int id);
        Task<GameDto> Create(CreateGameDto model);
        Task Update(GameDto model);
        Task Delete(int id); 

    }
}
