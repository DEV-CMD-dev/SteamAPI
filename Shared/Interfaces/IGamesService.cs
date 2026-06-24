using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Interfaces
{
    public interface IGamesService
    {
        Task<IList<GameDto>> GetAll();
    }
}
