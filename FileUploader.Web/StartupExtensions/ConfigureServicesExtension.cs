using Azure.Storage.Blobs;
using FileUploader.Core.BlobStorageObjects;
using FileUploader.Core.ServiceContracts;
using FileUploader.Core.Services;

namespace FileUploader.Web.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(x => new BlobServiceClient(configuration.GetSection("BlobStorageOptions")["ConnectionString"]));
            services.AddScoped<IUploaderService, UploaderService>();

            services.Configure<BlobStorageOptions>(configuration.GetSection("BlobStorageOptions"));

            return services;
        }
    }
}
