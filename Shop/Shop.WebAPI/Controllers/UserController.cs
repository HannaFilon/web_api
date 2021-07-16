using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop.Business.IServices;
using Shop.Business.Models;

namespace Shop.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> UpdateUser([FromBody] UserModel userModel)
        {
            return Ok();
        }

        [HttpPut("password")]
        public async Task<IActionResult> UpdatePassword([FromBody]PasswordUpdateModel passwordUpdateModel)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUserInfo(string token)
        {
            return Ok();
        }
    }
}
