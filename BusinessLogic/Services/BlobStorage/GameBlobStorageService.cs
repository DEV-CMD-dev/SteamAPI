using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BusinessLogic.Interfaces.BlobStorage;
using Microsoft.AspNetCore.Http;

namespace BusinessLogic.Services.BlobStorage
{
    public class GameBlobStorageService : IGameBlobStorageService
    {
        private readonly BlobContainerClient _containerClient;

        public GameBlobStorageService(Azure.Storage.Blobs.BlobServiceClient blobServiceClient)
        {
            _containerClient = blobServiceClient.GetBlobContainerClient("game-covers");
        }

        public async Task<string> UploadCoverAsync(IFormFile file, CancellationToken cancellationToken = default)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";

            var blobClient = _containerClient.GetBlobClient(fileName);

            await using var stream = file.OpenReadStream();

            await blobClient.UploadAsync(stream,
                new BlobUploadOptions
                {
                    HttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = file.ContentType
                    }
                }, cancellationToken);

            return blobClient.Uri.ToString();
        }

        public async Task<string> ReplaceCoverAsync(IFormFile file, string? oldImageUrl, CancellationToken cancellationToken = default)
        {
            if (!string.IsNullOrEmpty(oldImageUrl))
            {
                await DeleteCoverAsync(oldImageUrl, cancellationToken);
            }

            return await UploadCoverAsync(file, cancellationToken);
        }

        public async Task DeleteCoverAsync(string imageUrl, CancellationToken cancellationToken = default)
        {
            var fileName = Path.GetFileName(new Uri(imageUrl).LocalPath);

            var blobClient = _containerClient.GetBlobClient(fileName);

            await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
        }
    }
}
