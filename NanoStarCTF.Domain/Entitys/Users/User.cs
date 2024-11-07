using NanoStarCTF.Domain.Domain.Commons;
using NanoStarCTF.Domain.Entitys.Commons;
using NanoStarCTF.Domain.Entitys.CTFs;
using NanoStarCTF.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace NanoStarCTF.Domain.Entitys.Users
{
    public class User : Auditble
    {
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required, MaxLength(50)]
        public string Username { get; set; }
        [Required, MinLength(5)]
        public string Password { get; set; }
        public string Email { get; set; }
        public long? AddressId { get; set; }
        public HomeAddress? Address { get; set; }
        [MaxLength(200)]
        public string Bio { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public long? ImageId { get; set; }
        public Attachment? Image { get; set; }
        public UserRole Role { get; set; } = UserRole.User;
        public DateTime? DateOfBirth { get; set; }
        public long Points { get; set; } = 0;   
        public List<Challenge>? AcceptedProblems { get; set; }
       public User()
       {
        AcceptedProblems = new List<Challenge>();
       }
    }
}