using AutoFixture;
using FileUploader.Core.DataTransferObjects.UploadFormObjects;
using FileUploader.Core.Domain.Models;
using FileUploader.Core.ServiceContracts;
using FileUploader.Web.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploader.UnitTests.ControllersTests
{
    public class UploadControllerTests
    {
        private readonly Mock<IUploaderService> _mockUploaderService;

        private readonly UploadController _uploadController;

        private readonly Fixture _fixture;

        public UploadControllerTests()
        {
            _fixture = new Fixture();

            _mockUploaderService = new Mock<IUploaderService>();

            _uploadController = new UploadController(_mockUploaderService.Object);
        }

        [Fact]
        public async Task UploadForm_ValidUploadRequest_ShouldReturnOk()
        {
            // Arrange
            var uploadRequest = _fixture.Build<UploadRequest>()
                .With(x => x.File, new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, "This is a dummy file".Length, "Data", "dummy.docx"))
                .Create();

            _mockUploaderService
                .Setup(x => x.UploadFileToBlobStorageAsync(It.IsAny<UploadForm>()))
                .Returns(Task.FromResult(true));

            // Act
            var result = await _uploadController.UploadForm(uploadRequest);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task UploadForm_InvalidUploadRequest_ShouldReturnBadRequest()
        {
            // Arrange
            var uploadRequest = _fixture.Build<UploadRequest>()
                .With(x => x.File, new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, "This is a dummy file".Length, "Data", "dummy.txt"))
                .Create();

            _mockUploaderService
                .Setup(x => x.UploadFileToBlobStorageAsync(It.IsAny<UploadForm>()))
                .Returns(Task.FromResult(false));

            // Act
            var result = await _uploadController.UploadForm(uploadRequest);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task UploadForm_ModelValidationErrors_ShouldReturnBadRequest()
        {
            // Arrange
            var uploadRequest = _fixture.Build<UploadRequest>()
                .With(x => x.File, null as IFormFile)
                .With(x => x.Email, null as string)
                .Create();

            // Act
            var result = await _uploadController.UploadForm(uploadRequest);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }
    }
}
