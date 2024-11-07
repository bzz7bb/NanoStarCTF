using NanoStarCTF.Domain.Entitys.CTFs;
using NanoStarCTF.Domain.Entitys.Users;
using NanoStarCTF.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoStarCTF.Service.Dto_s.Users
{
    public class UserForUpdateDto
    {
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required, MaxLength(50)]
        public string Username { get; set; }
        [Required, MinLength(5)]
        public string Email { get; set; }
        public long? AddressId { get; set; }
        public HomeAddress Address { get; set; }
        [MaxLength(200)]
        public string Bio { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public long? ImageId { get; set; }
        public NanoStarCTF.Domain.Entitys.Commons.Attachment Image { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
