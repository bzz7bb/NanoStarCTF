using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NanoStarCTF.Domain.Entitys.CTFs;
using NanoStarCTF.Service.Dto_s;
using NanoStarCTF.Service.Dto_s.CTFs;
using NanoStarCTF.Service.Exceptions;
using NanoStarCTF.Service.IService;
#pragma warning disable
namespace NanoStarCTF.Api.Controllers.CTFControllers;

[Route("api/[controller]")]
[ApiController]
public class CTFController : ControllerBase
{
    private readonly ICTFService service;
    public CTFController(ICTFService service)
    {
        this.service = service;
    }

    /// <summary>
    /// Create new CTF
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("Insert"), Authorize(Roles = "Admin")]
    public async Task<CTFForViewDto> CreateAsync(CTFForCreationDto dto)
     => await service.CreateAsync(dto);

    /// <summary>
    /// Get By Id for CTF
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("GetById"),Authorize]
    public async Task<CTFForViewDto> GetById(int id)
       => await service.GetAsync(x => x.Id == id);

   /// <summary>
   /// Get All CTF
   /// </summary>
   /// <returns></returns>
    [HttpGet("GetAll"), Authorize]
    public async Task<IEnumerable<CTFForViewDto>> GetAllAsync()
        => service.GetAll();

    /// <summary>
    /// Update for CTF
    /// </summary>
    /// <param name="id"></param>
    /// <param name="ctfDto"></param>
    /// <returns></returns>
    [HttpPut("Update"), Authorize(Roles = "Admin")]
    public async Task<CTFForViewDto> UpdateAsync(long id, CTFForCreationDto ctfDto)
     => await service.Update(id, ctfDto);

    /// <summary>
    /// Delete for CTF
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("Delete"), Authorize(Roles = "Admin")]
    public async Task<bool> DeleteAsync(long id)
     => await service.DeleteAsync(x => x.Id == id);


    /// <summary>
    /// Get By Id for CTF
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("GetByIdFull"), Authorize]
    public async Task<IEnumerable<CTFForViewDto>> GetByIdFull(int id)
       => service.GetAll(x => x.Id == id);
}
