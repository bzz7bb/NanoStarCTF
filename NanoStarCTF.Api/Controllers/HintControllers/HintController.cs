using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NanoStarCTF.Service.Dto_s;
using NanoStarCTF.Service.Dto_s.HintsDto;
using NanoStarCTF.Service.Exceptions;
using NanoStarCTF.Service.IService;
#pragma warning disable
namespace NanoStarCTF.Api.Controllers.HintControllers;

[Route("api/[controller]")]
[ApiController]
public class HintController : ControllerBase
{
    private readonly IHintsService service;
    public HintController(IHintsService service)
    {
        this.service = service;
    }

 
    [HttpPost("Insert"),Authorize(Roles = "Admin")]
    public async Task<HintsForViewDto> CreateAsync(HintsForViewDto dto)
    {
        return await service.CreateAsync(dto);
    }


    [HttpGet("GetById"),Authorize]
    public async Task<HintsForViewDto> GetById(int id)
    {
        var result = await service.GetAsync(x => x.Id == id);
        return result;
    }


   
    [HttpGet("GetAll"),Authorize]
    public async Task<IEnumerable<HintsForViewDto>> GetAllAsync()
      =>  service.GetAll();

    
    [HttpPut("Update"),Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAsync(long id, HintsForViewDto HintsDto)
    {
        return Ok( service.Update(id, HintsDto));
 
    }

   
    [HttpDelete("Delete"),Authorize(Roles = "Admin")]
    public async Task DeleteAsync(long id)
    {
        await service.DeleteAsync(x => x.Id == id);
    }


    [HttpPost("InsertAndAdd"),Authorize(Roles ="Admin")]
    public async Task<HintsForViewDto> CreateAndADDAsync(HintsForCreationDto dto)
    {
        return await service.CreateAndAddAsync(dto);
    }

}
