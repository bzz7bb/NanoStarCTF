using NanoStarCTF.Service.Extensions;
using Microsoft.AspNetCore.Mvc;
using NanoStarCTF.Service.IService;
using Microsoft.AspNetCore.Authorization;

namespace NanoStarCTF.Api.Controllers.AttachmentControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {

        private readonly IAttachmentService attachmentService;

        public AttachmentController(IAttachmentService attachmentService)
        {
            this.attachmentService = attachmentService;
        }

        /// <summary>
        /// Upload image
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost,Authorize(Roles = "Admin")]
        public async ValueTask<IActionResult> UploadAsync(IFormFile formFile)
            => Ok(await attachmentService.UploadAsync(formFile.ToAttachmentOrDefault()));

        /// <summary>
        /// Update image
        /// </summary>
        /// <param name="id"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPut("{id}"),Authorize(Roles = "Admin")]
        public async ValueTask<IActionResult> UpdateAsync([FromRoute] int id, IFormFile formFile)
            => Ok(await attachmentService.UpdateAsync(id, formFile.ToAttachmentOrDefault().Stream));

   
        [HttpPost("CTF"),Authorize(Roles = "Admin")]
        public async ValueTask<IActionResult> UploadToCTFAsync(IFormFile formFile,long Id)
            => Ok(await attachmentService.UploadToCTFAsync(formFile.ToAttachmentOrDefault(),Id));

     
        [HttpPost("Group"),Authorize(Roles = "Admin")]
        public async ValueTask<IActionResult> UploadToGroupAsync(IFormFile formFile, long Id)
            => Ok(await attachmentService.UploadToGroupAsync(formFile.ToAttachmentOrDefault(), Id));


        [HttpPost("User"),Authorize(Roles = "Admin")]
        public async ValueTask<IActionResult> UploadToUserAsync(IFormFile formFile, long Id)
            => Ok(await attachmentService.UploadToUserAsync(formFile.ToAttachmentOrDefault(), Id));


    }
}
