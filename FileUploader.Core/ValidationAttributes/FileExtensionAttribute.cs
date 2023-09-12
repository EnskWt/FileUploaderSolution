using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploader.Core.ValidationAttributs
{
    public class FileExtensionAttribute : ValidationAttribute
    {
        private readonly string _requiredExtension;
        private string DefaultErrorMessage { get; } = "File must be a {0} type file.";

        public FileExtensionAttribute(string extension)
        {
            _requiredExtension = extension;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var fileExtension = Path.GetExtension(file.FileName);
                if (string.Equals(fileExtension, _requiredExtension, StringComparison.OrdinalIgnoreCase))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, _requiredExtension));
                }
            }

            return null;
        }
    }
}
