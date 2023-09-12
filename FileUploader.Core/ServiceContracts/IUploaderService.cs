using FileUploader.Core.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploader.Core.ServiceContracts
{
    public interface IUploaderService
    {
        /// <summary>
        /// Uploads a file to blob storage 
        /// </summary>
        /// <param name="uploadForm">UploadForm model which contains required email and file as IFormFile</param>
        /// <returns>Represents success of operation as true or false</returns>
        Task<bool> UploadFileToBlobStorageAsync(UploadForm uploadForm);
    };
}
