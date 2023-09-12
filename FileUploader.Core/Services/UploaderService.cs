using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FileUploader.Core.BlobStorageObjects;
using FileUploader.Core.Domain.Models;
using FileUploader.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace FileUploader.Core.Services
{
    public class UploaderService : IUploaderService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public UploaderService(IOptions<BlobStorageOptions> options, BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
            _containerName = options.Value.ContainerName;
        }

        public async Task<bool> UploadFileToBlobStorageAsync(UploadForm uploadForm)
        {
            var file = uploadForm.File;
            var email = uploadForm.Email;

            if (file == null || file.Length == 0)
            {
                throw new ArgumentNullException("File can't be null.");
            }

            if (email == null || email == string.Empty)
            {
                throw new ArgumentException("Email can't be null or empty.");
            }

            if (Path.GetExtension(file.FileName).ToLower() != ".docx")
            {
                throw new ArgumentException("File must be a .docx file.");
            }

            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

            var blobName = Guid.NewGuid().ToString() + ".docx";
            var blobClient = containerClient.GetBlobClient(blobName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobUploadOptions { Metadata = new Dictionary<string, string> { { "Email", email } } });
            }

            return true;
        }
    }
}
