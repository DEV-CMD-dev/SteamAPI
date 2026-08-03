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
        Task Patch(int id, string userId, PatchScreenshotDto model);
        Task Put(int id, string userId, PutScreenshotDto model);
        Task Delete(string userId, int id);
    }
}
