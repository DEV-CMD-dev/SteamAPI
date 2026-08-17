using BusinessLogic.DTOs.Achievement;
using BusinessLogic.DTOs.Screenshot;
using BusinessLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface IScreenshotService
    {
        Task<PaginatedList<ScreenshotDto>> GetAll(int pageNumber, int pageSize);
        Task<ScreenshotDto> GetById(int id);
        Task Create(string userId, CreateScreenshotDto dto);
        Task Update(int id, string userId, UpdateScreenshotDto model);
        Task Delete(string userId, int id);
    }
}
