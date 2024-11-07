using NanoStarCTF.Domain.Domain.Commons;
using System.ComponentModel.DataAnnotations;

namespace NanoStarCTF.Domain.Entitys.Users
{
    public class HomeAddress : Auditble
    {
        public string Country { get; set; }

        public string Region { get; set; }
        [MaxLength(50)]
        public string District { get; set; }

        [MaxLength(100)]
        public string AddressLine { get; set; }
    }
}