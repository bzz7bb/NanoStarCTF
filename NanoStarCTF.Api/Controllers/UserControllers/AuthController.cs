using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NanoStarCTF.Service.Dto_s.Users;
using NanoStarCTF.Service.IService;

namespace NanoStarCTF.Api.Controllers.UserControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IUserService userService;
        public AuthController(IAuthService authService, IUserService userService)
        {
            this.authService = authService;
            this.userService = userService;
        }

        /// <summary>
        /// Authorization
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async ValueTask<IActionResult> Login(UserForLoginDTO dto)
        {
            var token = await authService.GenerateToken(dto.Email, dto.Password);
            return Ok(new
            {
                token
            });
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="userForCreationDTO"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async ValueTask<IActionResult> CreateAsync(UserForCreationDto userForCreationDTO)
          {
           return Ok(await userService.CreateAsync(userForCreationDTO)); 
        }
    }
}
