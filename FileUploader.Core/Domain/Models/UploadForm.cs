using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploader.Core.Domain.Models
{
    public class UploadForm
    {
        public string? Email { get; set; }
        public IFormFile? File { get; set; }
    }
}
