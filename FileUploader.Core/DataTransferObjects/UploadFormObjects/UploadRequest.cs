using FileUploader.Core.Domain.Models;
using FileUploader.Core.ValidationAttributs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploader.Core.DataTransferObjects.UploadFormObjects
{
    public class UploadRequest
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required]
        [FileExtension(".docx")]
        public IFormFile File { get; set; } = null!;

        public UploadForm ToUploadForm()
        {
            return new UploadForm
            {
                Email = Email,
                File = File
            };
        }
    }
}
