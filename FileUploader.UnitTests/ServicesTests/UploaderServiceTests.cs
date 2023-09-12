using AutoFixture;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FileUploader.Core.BlobStorageObjects;
using FileUploader.Core.Domain.Models;
using FileUploader.Core.ServiceContracts;
using FileUploader.Core.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploader.UnitTests.ServicesTests
{
    public class UploaderServiceTests
    {
        private readonly Mock<BlobServiceClient> _mockBlobServiceClient;
        private readonly Mock<BlobContainerClient> _mockBlobContainerClient;
        private readonly Mock<BlobClient> _mockBlobClient;

        private readonly IUploaderService _uploaderService;
        private readonly Fixture _fixture;

        public UploaderServiceTests()
        {
            _fixture = new Fixture();

            _mockBlobServiceClient = new Mock<BlobServiceClient>();
            _mockBlobContainerClient = new Mock<BlobContainerClient>();
            _mockBlobClient = new Mock<BlobClient>();

            var options = Options.Create(new BlobStorageOptions
            {
                ConnectionString = "UseDevelopmentStorage=true",
                ContainerName = "test"
            });
            _uploaderService = new UploaderService(options, _mockBlobServiceClient.Object);
        }

        [Fact]
        public async Task UploadFileToBlobStorageAsync_ValidUploadForm_ShouldUploadFile()
        {
            // Arrange
            var uploadForm = _fixture.Build<UploadForm>()
                .With(x => x.File, new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, "This is a dummy file".Length, "Data", "dummy.docx"))
                .Create();

            _mockBlobServiceClient
                .Setup(x => x.GetBlobContainerClient(It.IsAny<string>()))
                .Returns(_mockBlobContainerClient.Object);
            _mockBlobContainerClient
                .Setup(x => x.GetBlobClient(It.IsAny<string>()))
                .Returns(_mockBlobClient.Object);

            var mockResponse = new Mock<Response<BlobContentInfo>>();
            _mockBlobClient
                .Setup(x => x.UploadAsync(It.IsAny<Stream>(), It.IsAny<BlobUploadOptions>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(mockResponse.Object));

            // Act
            var result = await _uploaderService.UploadFileToBlobStorageAsync(uploadForm);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UploadFileToBlobStorageAsync_FileNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            var uploadForm = _fixture.Build<UploadForm>()
                .With(x => x.File, null as IFormFile)
                .Create();

            _mockBlobServiceClient
                .Setup(x => x.GetBlobContainerClient(It.IsAny<string>()))
                .Returns(_mockBlobContainerClient.Object);
            _mockBlobContainerClient
                .Setup(x => x.GetBlobClient(It.IsAny<string>()))
                .Returns(_mockBlobClient.Object);

            var mockResponse = new Mock<Response<BlobContentInfo>>();
            _mockBlobClient
                .Setup(x => x.UploadAsync(It.IsAny<Stream>(), It.IsAny<BlobUploadOptions>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(mockResponse.Object));

            // Act
            var result = async () =>
            {
                await _uploaderService.UploadFileToBlobStorageAsync(uploadForm);
            };

            // Assert
            await result.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UploadFileToBlobStorageAsync_FileEmpty_ShouldThrowArgumentNullException()
        {
            // Arrange
            var uploadForm = _fixture.Build<UploadForm>()
                .With(x => x.File, new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.docx"))
                .Create();

            _mockBlobServiceClient
                .Setup(x => x.GetBlobContainerClient(It.IsAny<string>()))
                .Returns(_mockBlobContainerClient.Object);
            _mockBlobContainerClient
                .Setup(x => x.GetBlobClient(It.IsAny<string>()))
                .Returns(_mockBlobClient.Object);

            var mockResponse = new Mock<Response<BlobContentInfo>>();
            _mockBlobClient
                .Setup(x => x.UploadAsync(It.IsAny<Stream>(), It.IsAny<BlobUploadOptions>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(mockResponse.Object));

            // Act
            var result = async () =>
            {
                await _uploaderService.UploadFileToBlobStorageAsync(uploadForm);
            };

            // Assert
            await result.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UploadFileToBlobStorageAsync_EmailNull_ShouldThrowArgumentException()
        {
            // Arrange
            var uploadForm = _fixture.Build<UploadForm>()
                .With(x => x.File, new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, "This is a dummy file".Length, "Data", "dummy.docx"))
                .With(x => x.Email, null as string)
                .Create();

            _mockBlobServiceClient
                .Setup(x => x.GetBlobContainerClient(It.IsAny<string>()))
                .Returns(_mockBlobContainerClient.Object);
            _mockBlobContainerClient
                .Setup(x => x.GetBlobClient(It.IsAny<string>()))
                .Returns(_mockBlobClient.Object);

            var mockResponse = new Mock<Response<BlobContentInfo>>();
            _mockBlobClient
                .Setup(x => x.UploadAsync(It.IsAny<Stream>(), It.IsAny<BlobUploadOptions>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(mockResponse.Object));

            // Act
            var result = async () =>
            {
                await _uploaderService.UploadFileToBlobStorageAsync(uploadForm);
            };

            // Assert
            await result.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task UploadFileToBlobStorageAsync_EmailEmpty_ShouldThrowArgumentException()
        {
            // Arrange
            var uploadForm = _fixture.Build<UploadForm>()
                .With(x => x.File, new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, "This is a dummy file".Length, "Data", "dummy.docx"))
                .With(x => x.Email, string.Empty)
                .Create();

            _mockBlobServiceClient
                .Setup(x => x.GetBlobContainerClient(It.IsAny<string>()))
                .Returns(_mockBlobContainerClient.Object);
            _mockBlobContainerClient
                .Setup(x => x.GetBlobClient(It.IsAny<string>()))
                .Returns(_mockBlobClient.Object);

            var mockResponse = new Mock<Response<BlobContentInfo>>();
            _mockBlobClient
                .Setup(x => x.UploadAsync(It.IsAny<Stream>(), It.IsAny<BlobUploadOptions>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(mockResponse.Object));

            // Act
            var result = async () =>
            {
                await _uploaderService.UploadFileToBlobStorageAsync(uploadForm);
            };

            // Assert
            await result.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task UploadFileToBlobStorageAsync_FileExtensionNotDocx_ShouldThrowArgumentException()
        {
            // Arrange
            var uploadForm = _fixture.Build<UploadForm>()
                .With(x => x.File, new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, "This is a dummy file".Length, "Data", "dummy.txt"))
                .With(x => x.Email, string.Empty)
                .Create();

            _mockBlobServiceClient
                .Setup(x => x.GetBlobContainerClient(It.IsAny<string>()))
                .Returns(_mockBlobContainerClient.Object);
            _mockBlobContainerClient
                .Setup(x => x.GetBlobClient(It.IsAny<string>()))
                .Returns(_mockBlobClient.Object);

            var mockResponse = new Mock<Response<BlobContentInfo>>();
            _mockBlobClient
                .Setup(x => x.UploadAsync(It.IsAny<Stream>(), It.IsAny<BlobUploadOptions>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(mockResponse.Object));

            // Act
            var result = async () =>
            {
                await _uploaderService.UploadFileToBlobStorageAsync(uploadForm);
            };

            // Assert
            await result.Should().ThrowAsync<ArgumentException>();
        }
    }
}
