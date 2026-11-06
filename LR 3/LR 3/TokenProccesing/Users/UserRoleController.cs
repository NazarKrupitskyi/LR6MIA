using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static LR_3.TokenProccesing.Tokens_Auth.RoleFlags;

namespace LR_3.TokenProccesing.Users
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = nameof(Roles.Admin))]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService) => _userService = userService;

        [HttpPost("setRoles")]
        public async Task<IActionResult> SetRoles(string id, Roles roles)
        {
            try
            {
                var user = await _userService.SetRolesAsync(id, roles);
                return Ok(new { username = user.Username, roles = user.Role, rolesValue = (int)user.Role });
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }

        [HttpPost("addRole")]
        public async Task<IActionResult> AddRole(string id, Roles role)
        {
            try
            {
                var user = await _userService.AddRoleAsync(id, role);
                return Ok(new { username = user.Username, roles = user.Role, rolesValue = (int)user.Role });
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }

        [HttpPost("removeRole")]
        public async Task<IActionResult> RemoveRole(string id, Roles role)
        {
            try
            {
                var user = await _userService.RemoveRoleAsync(id, role);
                return Ok(new { username = user.Username, roles = user.Role, rolesValue = (int)user.Role });
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
        }
    }
}
