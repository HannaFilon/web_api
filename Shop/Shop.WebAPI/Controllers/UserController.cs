using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Business.IServices;
using Shop.Business.Models;
using Shop.WebAPI.Auth;

namespace Shop.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;

        public UserController(IUserService userService, IMapper mapper, IAuthManager authManager)
        {
            _userService = userService;
            _mapper = mapper;
            _authManager = authManager;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserModel userModel)
        {
            var token = await HttpContext.GetTokenAsync("Bearer", "access_token");
            var userId = GetCurrentUserId(token);
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("This method is unavailable.");
            }

            var userDto = await _userService.UpdateUser(userId, userModel);
            _mapper.Map(userDto, userModel);

            return Ok(userModel);
        }

        [HttpPost("password")]
        public async Task<IActionResult> UpdatePassword([FromBody] PasswordUpdateModel passwordUpdateModel)
        {
            var token = await HttpContext.GetTokenAsync("Bearer", "access_token");
            var userId = GetCurrentUserId(token);
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("This method is unavailable.");
            }

            var result = await _userService.UpdatePassword(userId, passwordUpdateModel);
            if (!result.Succeeded)
            {
                return BadRequest("Password can't be updated.");
            }

            return StatusCode(204);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            var token = await HttpContext.GetTokenAsync("Bearer", "access_token");
            var userId = GetCurrentUserId(token);
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("This method is unavailable.");
            }

            var userDto = await _userService.GetById(userId);

            if (userDto == null)
            {
                return BadRequest("User not found.");
            }

            var userModel = _mapper.Map<UserModel>(userDto);

            return Ok(userModel);
        }

        private string GetCurrentUserId(string token)
        {
            var jwtToken = _authManager.DecodeJwtToken(token);
            var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

            return userId;
        }
    }
}
