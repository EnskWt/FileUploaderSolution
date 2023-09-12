using FileUploader.Core.DataTransferObjects.UploadFormObjects;
using FileUploader.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileUploader.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploaderService _uploaderService;

        public UploadController(IUploaderService uploaderService)
        {
            _uploaderService = uploaderService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("API avaliable now.");
        }

        [HttpPost]
        public async Task<IActionResult> UploadForm([FromForm] UploadRequest uploadRequest)
        {
            var uploadForm = uploadRequest.ToUploadForm();

            var result = await _uploaderService.UploadFileToBlobStorageAsync(uploadForm);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
