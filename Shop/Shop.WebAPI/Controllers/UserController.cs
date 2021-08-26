using System;
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

            if (userModel == null)
            {
                return BadRequest("Not enough parameters to update user.");
            }

            var userDto = await _userService.UpdateUser(Guid.NewGuid().ToString(), userModel);
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
            var token = await HttpContext.GetTokenAsync("Bearer", "access_token");
            var userId = GetCurrentUserId(token);
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("This method is unavailable.");
            }

            var userDto = await _userService.GetById(userId);
            var userModel = _mapper.Map<UserModel>(userDto);

            return Ok(userModel);
        }

        private string GetCurrentUserId(string token)
        {
            var jwtToken = _authManager.DecodeJwtToken(token);
            if (jwtToken == null)
            {
                throw new Exception("Wrong Token.");
            }

            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == "id").Value;

            return userId;
        }
    }
}
