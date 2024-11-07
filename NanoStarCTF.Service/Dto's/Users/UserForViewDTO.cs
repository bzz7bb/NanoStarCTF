using NanoStarCTF.Domain.Entitys.Users;
using NanoStarCTF.Domain.Entitys.Commons;
using NanoStarCTF.Domain.Enums;
using System.ComponentModel.DataAnnotations;


#pragma warning disable
namespace NanoStarCTF.Service.Dto_s.Users
{
    public class UserForViewDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public long? AddressId { get; set; }
        public HomeAddress Address { get; set; }
        public string Bio { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public long? ImageId { get; set; }
        public Attachment? Image { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public long Points { get; set; } = 0;
    }
}
