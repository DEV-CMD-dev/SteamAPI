using Azure.Storage.Blobs.Models;
using BusinessLogic.Interfaces.BlobStorage;
using DataAccess.Enums;
using Microsoft.Extensions.Configuration;

namespace BusinessLogic.Services.BlobStorage
{
    public class BlobServiceClient : IBlobServiceClient
    {
        private readonly Azure.Storage.Blobs.BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _configuration;

        public BlobServiceClient (Azure.Storage.Blobs.BlobServiceClient blobServiceClient, IConfiguration configuration)
        {
            _blobServiceClient = blobServiceClient;
            _configuration = configuration;
        }

        public async Task<string> UploadAsync(Stream file, string fileName, string contentType, BlobContainer container, CancellationToken cancellationToken = default)
        {
            var containerName = GetContainerName(container);
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            await containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(file,
                new BlobUploadOptions
                {
                    HttpHeaders = new BlobHttpHeaders{
                        ContentType = contentType
                    }
                }, cancellationToken);

            return blobClient.Uri.ToString();
        }

        public async Task DeleteAsync(string fileName, BlobContainer container, CancellationToken cancellationToken = default)
        {
            var containerName = GetContainerName(container);
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
        }

        private string GetContainerName(BlobContainer container)
        {
            return container switch
            {
                BlobContainer.Avatars => _configuration["AzureBlobStorage:Containers:Avatars"]!,
                BlobContainer.GameCovers => _configuration["AzureBlobStorage:Containers:GameCovers"]!,
                BlobContainer.GameScreenshots =>_configuration["AzureBlobStorage:Containers:GameScreenshots"]!,
                BlobContainer.TagCovers =>_configuration["AzureBlobStorage:Containers:TagCovers"]!,
                _ => throw new ArgumentOutOfRangeException(nameof(container))
            };
        }
    }
}
