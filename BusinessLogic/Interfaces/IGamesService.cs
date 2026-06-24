using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces
{
    public interface IGamesService
    {
        Task<IList<GameDto>> GetAll();
    }
}
