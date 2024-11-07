using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NanoStarCTF.Service.Dto_s.CTFs;
using NanoStarCTF.Service.Dto_s;
using NanoStarCTF.Service.IService;
using NanoStarCTF.Domain.Entitys;
using NanoStarCTF.Service.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace NanoStarCTF.Api.Controllers.CheckerControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckerController : ControllerBase
    {

        private readonly ICheckerService service;
        public CheckerController(ICheckerService service)
        {
            this.service = service;
        }

      /// <summary>
      /// Check for CTF
      /// </summary>
      /// <param name="dto"></param>
      /// <returns></returns>
        [HttpPost("Check"),Authorize]
        public async Task<IActionResult> Check(Checker dto)
        {
            return Ok( await service.Check(dto));
        }

    }
}
