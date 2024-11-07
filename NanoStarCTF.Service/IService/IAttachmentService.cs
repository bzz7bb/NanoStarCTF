using NanoStarCTF.Domain.Entitys.Commons;
using NanoStarCTF.Service.Dto_s.Attachments;

namespace NanoStarCTF.Service.IService
{
    public interface IAttachmentService
    {
        ValueTask<Attachment> UploadAsync(AttachmentForCreateDto dto);
        ValueTask<Attachment> UploadToCTFAsync(AttachmentForCreateDto dto,long Id);
        ValueTask<Attachment> UploadToGroupAsync(AttachmentForCreateDto dto, long Id);
        ValueTask<Attachment> UploadToUserAsync(AttachmentForCreateDto dto, long Id);
        ValueTask<Attachment> UpdateAsync(int id, Stream stream);
        ValueTask<Attachment> CreateAsync(string filePath);

    }
}
