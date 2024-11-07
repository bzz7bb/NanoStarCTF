using NanoStarCTF.DataAccess.Repositories;
using NanoStarCTF.Domain.Entitys.Commons;
using NanoStarCTF.Domain.Entitys.CTFs;
using NanoStarCTF.Domain.Entitys.Groups;
using NanoStarCTF.Domain.Entitys.Users;
using NanoStarCTF.Service.Dto_s.Attachments;
using NanoStarCTF.Service.Exceptions;
using NanoStarCTF.Service.Helpers;
using NanoStarCTF.Service.IService;

namespace NanoStarCTF.Service.Service
{
    public class AttachmentService : IAttachmentService
    {

        private readonly IGenericRepository<Attachment> attachmentRepository;
        private readonly IGenericRepository<Challenge> ctfrepository;
        private readonly IGenericRepository<Group> genericRepository;
        private readonly IGenericRepository<User> userRepository;


        public AttachmentService(IGenericRepository<Attachment> attachmentRepository,IGenericRepository<Challenge> ctfrepository,
            IGenericRepository<Group> genericRepository,IGenericRepository<User> userRepository)
        {
            this.ctfrepository = ctfrepository;
            this.attachmentRepository = attachmentRepository;
            this.genericRepository = genericRepository;
            this.userRepository = userRepository;
        }

        public async ValueTask<Attachment> CreateAsync(string filePath)
        {

            var file = new Attachment()
            {
                Path = filePath
            };

            file = await attachmentRepository.CreateAsync(file);
            await attachmentRepository.SaveChangesAsync();




            return file;
        }

        public async ValueTask<Attachment> UpdateAsync(int id, Stream stream)
        {
            var existAttachment = await attachmentRepository.GetAsync(a => a.Id == id);

            if (existAttachment is null)
                throw new HttpStatusCodeException(404, "Attachment not found.");

            string fileName = existAttachment.Path;
            string filePath = Path.Combine(EnvironmentHelper.WebRootPath, fileName);

            // copy image to the destination as stream
            FileStream fileStream = File.OpenWrite(filePath);
            await stream.CopyToAsync(fileStream);

            // clear
            await fileStream.FlushAsync();
            fileStream.Close();

            await attachmentRepository.SaveChangesAsync();

            return existAttachment;
        }

        public async ValueTask<Attachment> UploadAsync(AttachmentForCreateDto dto)
        {
            string fileName = Guid.NewGuid().ToString("N") + ".png";
            string filePath = Path.Combine(EnvironmentHelper.AttachmentPath, fileName);

            if (!Directory.Exists(EnvironmentHelper.AttachmentPath))
                Directory.CreateDirectory(EnvironmentHelper.AttachmentPath);

            // copy image to the destination as stream  
            FileStream fileStream = File.OpenWrite(filePath);
            await dto.Stream.CopyToAsync(fileStream);

            // clear
            await fileStream.FlushAsync();
            fileStream.Close();

            return await CreateAsync(Path.Combine(EnvironmentHelper.FilePath, fileName));

        }

        public async ValueTask<Attachment> UploadToCTFAsync(AttachmentForCreateDto dto,long Id)
        {
            string fileName = Guid.NewGuid().ToString("N") + ".png";
            string filePath = Path.Combine(EnvironmentHelper.AttachmentPath, fileName);

            if (!Directory.Exists(EnvironmentHelper.AttachmentPath))
                Directory.CreateDirectory(EnvironmentHelper.AttachmentPath);

            // copy image to the destination as stream  
            FileStream fileStream = File.OpenWrite(filePath);
            await dto.Stream.CopyToAsync(fileStream);

            // clear
            await fileStream.FlushAsync();
            fileStream.Close();

            var attech = await CreateAsync(Path.Combine(EnvironmentHelper.FilePath, fileName));

            var ctf = await ctfrepository.GetAsync(ctf => ctf.Id == Id);

            ctf.Image = attech;
 
            ctfrepository.Update(ctf);

            await ctfrepository.SaveChangesAsync();

            return attech;
        }

        public async ValueTask<Attachment> UploadToGroupAsync(AttachmentForCreateDto dto, long Id)
        {
            string fileName = Guid.NewGuid().ToString("N") + ".png";
            string filePath = Path.Combine(EnvironmentHelper.AttachmentPath, fileName);

            if (!Directory.Exists(EnvironmentHelper.AttachmentPath))
                Directory.CreateDirectory(EnvironmentHelper.AttachmentPath);

            // copy image to the destination as stream  
            FileStream fileStream = File.OpenWrite(filePath);
            await dto.Stream.CopyToAsync(fileStream);

            // clear
            await fileStream.FlushAsync();
            fileStream.Close();

            var attech = await CreateAsync(Path.Combine(EnvironmentHelper.FilePath, fileName));

            var group = await genericRepository.GetAsync(ctf => ctf.Id == Id);

            group.Image = attech;

            genericRepository.Update(group);

            await genericRepository.SaveChangesAsync();

            return attech;
        }

        public async ValueTask<Attachment> UploadToUserAsync(AttachmentForCreateDto dto, long Id)
        {

            string fileName = Guid.NewGuid().ToString("N") + ".png";
            string filePath = Path.Combine(EnvironmentHelper.AttachmentPath, fileName);

            if (!Directory.Exists(EnvironmentHelper.AttachmentPath))
                Directory.CreateDirectory(EnvironmentHelper.AttachmentPath);

            // copy image to the destination as stream  
            FileStream fileStream = File.OpenWrite(filePath);
            await dto.Stream.CopyToAsync(fileStream);

            // clear
            await fileStream.FlushAsync();
            fileStream.Close();

            var attech = await CreateAsync(Path.Combine(EnvironmentHelper.FilePath, fileName));

            var user = await userRepository.GetAsync(ctf => ctf.Id == Id);

            user.Image = attech;

            userRepository.Update(user);

            await userRepository.SaveChangesAsync();

            return attech;
        }
    }
}
