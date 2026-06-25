using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class BlobService : IBlobService
    {
        private readonly IConfiguration _config;

        public BlobService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<string> UploadBlobAsync(IFormFile file)
        {
            var connectionString = _config["AzureStorage:ConnectionString"];
            var containerName = _config["AzureStorage:ContainerName"];
      
            var blobServiceClient = new BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            var blobHttpHeader = new BlobHttpHeaders
            {
                ContentType = file.ContentType
            };

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var blobClient = containerClient.GetBlobClient(fileName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobUploadOptions
                {
                    HttpHeaders = blobHttpHeader
                });
            }

            return blobClient.Uri.ToString();
        }
    }
}
