using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface IBlobService
    {
        Task<string> UploadBlobAsync(IFormFile file);
    }
}
