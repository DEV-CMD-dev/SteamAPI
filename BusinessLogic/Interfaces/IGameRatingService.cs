using BusinessLogic.DTOs.GameRate;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface IGameRatingService
    {
        Task UpdateGameRatingAsync(int gameId);
    }
}
