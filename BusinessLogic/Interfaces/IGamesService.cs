using BusinessLogic.DTOs.Game;

namespace BusinessLogic.Interfaces
{
    public interface IGamesService
    {
        Task<IList<GameDto>> GetAll();
        Task<GameDto?> Get(int id);
        Task<GameDto> Create(CreateGameDto model,string url);
        Task Update(EditGameDto model,string url);
        Task Delete(int id); 

    }
}
