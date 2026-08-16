using DataAccess.Enums;

namespace BusinessLogic.Interfaces.BlobStorage
{
    public interface IBlobServiceClient
    {
        Task<string> UploadAsync (Stream file, string fileName ,string contentType, BlobContainer container, CancellationToken cancellationToken = default);
        Task DeleteAsync(string fileName, BlobContainer container, CancellationToken cancellationToken = default);
    }
}
