using BusinessLogic.DTOs.Achievement;
using BusinessLogic.DTOs.GameVersion;
using BusinessLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface IGameVersionService
    {
        Task<PaginatedList<GameVersionDto>> GetAll(int pageNumber, int pageSize);
        Task<GameVersionDto> GetById(int id);
        Task Create(string userId, CreateGameVersionDto dto);
        Task Patch(int id, string userId, PatchGameVersionDto model);
        Task Put(int id, string userId, PutGameVersionDto model);
        Task Delete(string userId, int id);
    }
}
