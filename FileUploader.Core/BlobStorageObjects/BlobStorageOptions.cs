using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploader.Core.BlobStorageObjects
{
    public class BlobStorageOptions
    {
        public string ConnectionString { get; set; } = null!;
        public string ContainerName { get; set; } = null!;
    }
}
