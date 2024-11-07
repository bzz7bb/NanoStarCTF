using NanoStarCTF.Domain.Domain.Commons;
using NanoStarCTF.Domain.Entitys.Commons;
using NanoStarCTF.Domain.Entitys.CTFs;
namespace NanoStarCTF.Domain.Entitys.Groups
{
    public class Group : Auditble
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        public long? ImageId { get; set; }
        public Attachment? Image { get; set; }
        public virtual List<Challenge>? CTFsList { get; set; }
        public Group()
        {
            CTFsList = new List<Challenge>();
        }
    }
}