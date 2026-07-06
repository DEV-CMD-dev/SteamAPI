using BusinessLogic.DTOs.Game;
using BusinessLogic.DTOs.Tag;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface ITagService
    {
        Task<IList<TagDto>> GetAll();
        Task<TagDto> Get(int id);
        Task Create(CreateTagDto dto);
        Task Update(TagDto dto);
        Task Delete(int id);
    }
}
