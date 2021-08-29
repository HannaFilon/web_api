using System.Threading.Tasks;
using AutoMapper;
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
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper, IAuthManager authManager) : base(authManager)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserModel userModel)
        {
            var userId = await GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("This method is unavailable.");
            }

            if (userModel == null)
            {
                return BadRequest("Not enough parameters to update user.");
            }

            var userDto = await _userService.UpdateUser(userId, userModel);
            _mapper.Map(userDto, userModel);

            return Ok(userModel);
        }

        [HttpPost("password")]
        public async Task<IActionResult> UpdatePassword([FromBody] PasswordUpdateModel passwordUpdateModel)
        {
            var userId = await GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("This method is unavailable.");
            }

            if (passwordUpdateModel == null)
            {
                return BadRequest("Not enough parameters to update password.");
            }

            await _userService.UpdatePassword(userId, passwordUpdateModel);

            return StatusCode(204);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            var userId = await GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("This method is unavailable.");
            }

            var userDto = await _userService.GetById(userId);
            var userModel = _mapper.Map<UserModel>(userDto);

            return Ok(userModel);
        }

    }
}
