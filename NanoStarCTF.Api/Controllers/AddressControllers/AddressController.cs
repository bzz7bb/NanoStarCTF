using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NanoStarCTF.Service.Dto_s.Address;
using NanoStarCTF.Service.IService;
#pragma warning disable
namespace NanoStarCTF.Api.Controllers.AddressControllers;

[Route("api/[controller]")]
[ApiController]
public class AddressController : ControllerBase
{
    private readonly IAddressService service;
    public AddressController(IAddressService service)
    {
        this.service = service;
    }

    [HttpPost("Insert"),Authorize]
    public async Task<AddressForCreationDto> CreateAsync(AddressForCreationDto dto)
    {
        return await service.CreateAsync(dto);
    }


    [HttpGet("GetById"),Authorize]
    public async Task<AddressForCreationDto> GetById(long id)
    {
        var result = await service.GetAsync(x => x.Id == id);
        return result;

    }


    [HttpGet("GetAll"),Authorize]
    public  async Task<IEnumerable<AddressForCreationDto>> GetAllAsync()
    {
        return service.GetAll();
    }


    [HttpPut("Update"),Authorize]
    public async Task<AddressForCreationDto> UpdateAsync(long id, AddressForCreationDto AddresDto)
    {
        return await service.Update(id, AddresDto);
    }


    [HttpDelete("Delete"),Authorize]
    public async Task DeleteAsync(long id)
    {
        await service.DeleteAsync(x => x.Id == id);
    }

}
