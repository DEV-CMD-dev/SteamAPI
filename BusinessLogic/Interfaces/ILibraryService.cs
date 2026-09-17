using BusinessLogic.DTOs.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface ILibraryService
    {
        Task<List<LibraryGameDto>> GetUserGames(string userId);
    }
}
