using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CognifyzDatabaseIntegrationAndUserAuthentication.Dto;
using CognifyzDatabaseIntegrationAndUserAuthentication.Implementation.Interface;
using CognifyzDatabaseIntegrationAndUserAuthentication.Models;

namespace CognifyzDatabaseIntegrationAndUserAuthentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")] 
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUser();
            return Ok(users);
        }


        [HttpGet("{id}")]
        [Authorize] 
        public async Task<ActionResult<UserDto>> GetUserDetail(string id)
        {
            var user = await _userService.GetUserDetail(id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }
            return Ok(user);
        }

        [HttpPost("login")]
        [AllowAnonymous] 
        public async Task<ActionResult<Status>> Login([FromBody] LoginModel login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var status = await _userService.Login(login);
            if (status.Success)
            {
                return Ok(status);
            }
            else
            {
                return Unauthorized(status);
            }
        }

       
        [HttpPost("logout")]
        [Authorize] 
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return Ok(new { Message = "Logout successful" });
        }
    }
}
