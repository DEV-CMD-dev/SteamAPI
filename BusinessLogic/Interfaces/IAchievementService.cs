using BusinessLogic.DTOs.Achievement;
using BusinessLogic.DTOs.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface IAchievementService
    {
        Task<IList<AchievementDto>> GetAll();
        Task<AchievementDto?> Get(int id);
        Task<AchievementDto> Create(CreateAchivementDto model);
        Task Update(AchievementDto model);
        Task Delete(int id);
    }
}
