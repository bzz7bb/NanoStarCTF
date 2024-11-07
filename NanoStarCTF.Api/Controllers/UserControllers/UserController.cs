using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NanoStarCTF.Domain.Entitys.CTFs;
using NanoStarCTF.Domain.Entitys.Users;
using NanoStarCTF.Domain.Enums;
using NanoStarCTF.Service.Dto_s;
using NanoStarCTF.Service.Dto_s.Users;
using NanoStarCTF.Service.Exceptions;
using NanoStarCTF.Service.IService;
#pragma warning disable
namespace NanoStarCTF.Api.Controllers.UserControllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService service;
    public UserController(IUserService service)
    {
        this.service = service;
    }

   /// <summary>
   /// Create user
   /// </summary>
   /// <param name="dto"></param>
   /// <returns></returns>
    [HttpPost("Insert"),AllowAnonymous]
    public async Task<IActionResult> CreateAsync(UserForCreationDto dto)
    {
        return Ok( await service.CreateAsync(dto));
    }

    /// <summary>
    /// Get for PI Information
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("GetById"),Authorize]
    public async Task<IActionResult> GetById(long id)
    {
      return Ok(await service.GetAsync(x => x.Id == id));
    }

    /// <summary>
    /// Get All user information
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetAll"),Authorize(Roles = "Admin")]
    public async Task<IEnumerable<UserForViewDto>> GetAllAsync()
        => service.GetAll();
    
    /// <summary>
    /// Update PI information
    /// </summary>
    /// <param name="id"></param>
    /// <param name="ctfDto"></param>
    /// <returns></returns>
    [HttpPut("Update"),Authorize]
    public async Task<IActionResult> UpdateAsync(long id, UserForUpdateDto ctfDto)
    {
        return Ok( await service.Update(id, ctfDto));
  
    }

    /// <summary>
    /// Delete for PI information
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("Delete"),Authorize]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        return Ok( await service.DeleteAsync(x => x.Id == id));
    }
}
