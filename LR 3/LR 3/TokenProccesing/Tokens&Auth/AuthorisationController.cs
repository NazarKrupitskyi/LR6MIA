using LR_3.TokenProccesing.Tokens_Auth;
using LR_3.TokenProccesing.Users;
using Microsoft.AspNetCore.Mvc;

namespace LR_3.TokenProccesing
{
    

    [ApiController]
    [Route("api/[controller]")]
    public class AuthorisationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthorisationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDTO user)
        {
            try
            {
                var result = await _userService.CreateAsync(user);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO dto)
        {
            try
            {
                var result = await _userService.LoginAsync(dto);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromHeader(Name = "Authorization")] string authHeader, [FromBody] string refreshToken)
        {
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return BadRequest("Missing or invalid Authorization header");

            var accessToken = authHeader.Substring("Bearer ".Length);

            try
            {
                var result = await _userService.RefreshTokenAsync(accessToken, refreshToken);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

    }
}
