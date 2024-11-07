using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NanoStarCTF.Domain.Entitys.CTFs;
using NanoStarCTF.Domain.Entitys.Groups;
using NanoStarCTF.Service.Dto_s;
using NanoStarCTF.Service.Dto_s.Groups;
using NanoStarCTF.Service.IService;
#pragma warning disable
namespace NanoStarCTF.Api.Controllers.GroupControllers;

[Route("api/[controller]")]
[ApiController]
public class GroupController : ControllerBase
{
    private readonly IGroupService service;
    public GroupController(IGroupService service)
    {
        this.service = service;
    }

   /// <summary>
   /// Create new group for CTF
   /// </summary>
   /// <param name="dto"></param>
   /// <returns></returns>
    [HttpPost("Insert"), Authorize(Roles = "Admin")]
    public async Task<GroupForViewDto> CreateAsync(GroupForCreationDto dto)
    {
        return await service.CreateAsync(dto);
    }

   /// <summary>
   /// Get by id for CTF group
   /// </summary>
   /// <param name="id"></param>
   /// <returns></returns>
    [HttpGet("GetById"), Authorize]
    public async Task<GroupForViewDto> GetById(long id)
    {
        var result = await service.GetAsync(x => x.Id == id);
        return result;

    }


    /// <summary>
    /// Get all for CTF group
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetAll"), Authorize]
    public async Task<IEnumerable<GroupForViewDto>> GetAllAsync()
    {
        var users = service.GetAll(null);
        return users;
    }

   /// <summary>
   /// Update for CTF Group
   /// </summary>
   /// <param name="id"></param>
   /// <param name="GroupDto"></param>
   /// <returns></returns>
    [HttpPut("Update"), Authorize(Roles ="Admin")]
    public async Task<GroupForViewDto> UpdateAsync(long id, GroupForCreationDto GroupDto)
    {
        return await service.Update(id, GroupDto);
    }

  /// <summary>
  /// Delete for CTF Group
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
    [HttpDelete("Delete"), Authorize(Roles ="Admin")]
    public async Task DeleteAsync(long id)
    {
        await service.DeleteAsync(x => x.Id == id);
    }

}
