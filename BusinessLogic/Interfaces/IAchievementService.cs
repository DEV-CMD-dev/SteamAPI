using BusinessLogic.DTOs.Achievement;
using BusinessLogic.DTOs.Game;
using BusinessLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface IAchievementService
    {
        Task<PaginatedList<AchievementDto>> GetAll(int pageNumber, int pageSize);
        Task<AchievementDto> GetById(int id);
        Task Create(string userId, CreateAchievementDto dto);
        Task Patch(int id, string userId, PatchAchievementDto model);
        Task Put(int id, string userId, PutAchievementDto model);
        Task Delete(string userId, int id);
    }
}
