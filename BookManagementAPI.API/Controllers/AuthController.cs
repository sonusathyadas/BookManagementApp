using Microsoft.AspNetCore.Mvc;
using BookManagementAPI.API.DTOs;
using BookManagementAPI.Core.Interfaces;
using System.Threading.Tasks;

namespace BookManagementAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto registerDto)
        {
            var result = await _authService.Register(
                registerDto.Username,
                registerDto.Password,
                registerDto.Firstname,
                registerDto.Lastname,
                registerDto.Email);

            if (result.Token == null)
            {
                return BadRequest("Username or email already exists");
            }

            return Ok(new AuthResponseDto
            {
                Token = result.Token,
                Username = result.Username!,
                Email = result.Email!
            });
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
        {
            var result = await _authService.Login(loginDto.Username, loginDto.Password);
            if (result.Token == null)
            {
                return Unauthorized("Invalid username or password");
            }

            return Ok(new AuthResponseDto
            {
                Token = result.Token,
                Username = result.Username!,
                Email = result.Email!
            });
        }
    }
}
