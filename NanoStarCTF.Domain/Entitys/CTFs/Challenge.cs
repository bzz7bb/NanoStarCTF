using NanoStarCTF.Domain.Domain.Commons;
using NanoStarCTF.Domain.Entitys.Commons;
using NanoStarCTF.Domain.Entitys.Groups;
using NanoStarCTF.Domain.Entitys.Users;

namespace NanoStarCTF.Domain.Entitys.CTFs
{
    public class Challenge : Auditble
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Hints>? Hints { get; set; }
        public string Link { get; set; }
        public long? ImageId { get; set; }
        public long GroupId { get; set; }
        public virtual Group Group { get; set; }
        public Attachment? Image { get; set; }
        public string FlagChek { get; set; }
        public List<User> Users { get; set; }
        public Challenge()
        {
            Hints = new List<Hints>();
            Users = new List<User>();
        }

    }
}