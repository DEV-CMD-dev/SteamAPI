using Microsoft.AspNetCore.Http;

namespace BusinessLogic.Interfaces.BlobStorage
{
    public interface IGameBlobStorageService
    {
        Task<string> UploadCoverAsync(IFormFile file, CancellationToken cancellationToken = default);
        Task<string> ReplaceCoverAsync(IFormFile file, string? oldImageUrl, CancellationToken cancellationToken = default);
        Task DeleteCoverAsync(string imageUrl, CancellationToken cancellationToken = default);
    }
}
