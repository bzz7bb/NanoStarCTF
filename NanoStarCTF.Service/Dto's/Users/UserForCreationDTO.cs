
using NanoStarCTF.Domain.Entitys.CTFs;
using NanoStarCTF.Domain.Entitys.Users;
using NanoStarCTF.Domain.Enums;
using System.ComponentModel.DataAnnotations;
#pragma warning disable
namespace NanoStarCTF.Service.Dto_s.Users;

public class UserForCreationDto
{
    [MaxLength(50)]
    public string FirstName { get; set; }
    [MaxLength(50)]
    public string LastName { get; set; }
    [Required, MaxLength(50)]
    public string Username { get; set; }
    [Required, MinLength(5)]
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string Email { get; set; }
    [MaxLength(200)]
    public string Bio { get; set; }
    [Required]
    public Gender Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
  
}
