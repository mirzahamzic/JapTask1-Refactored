using JapTask1.Core.Dtos;
using JapTask1.Core.Entities;
using JapTask1.Core.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JapTask1.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CORS")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserDto request)
        {
            var response = await _authService.Register(new User { Name = request.Name }, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserDto request)
        {
            var response = await _authService.Login(request.Name, request.Password);

            //if (!response.Success)
            //{
            //    return BadRequest(response);
            //}
            return Ok(response);
        }

    }
}
