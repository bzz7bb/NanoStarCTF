using Microsoft.AspNetCore.Http;
using NanoStarCTF.Service.Dto_s.Attachments;

namespace NanoStarCTF.Service.Extensions
{
    public static class FormFileExtensions
    {
        public static AttachmentForCreateDto ToAttachmentOrDefault(this IFormFile formFile)
        {

            if (formFile?.Length > 0)
            {
                using var ms = new MemoryStream();
                formFile.CopyTo(ms);
                var fileBytes = ms.ToArray();

                return new AttachmentForCreateDto()
                {
                    Path = formFile.FileName,
                    Stream = new MemoryStream(fileBytes)
                };
            }

            return null;
        }
    }
}
