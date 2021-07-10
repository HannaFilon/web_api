using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Threading.Tasks;
using System.IO;
using Shop.Business.Implementation;
using Shop.Business.Models;

namespace Shop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("signIn")]
        public async Task<ActionResult> SignIn([FromBody] LoginModel loginModel)
        {
            UserDTO userDTO = await _userService.GetByEmail(loginModel.Email);

            bool isCorrect = await _userService.CheckPassword(userDTO.Email, loginModel.Password);
            if (!isCorrect)
                return StatusCode(401, "Wrong message");
            return Ok();
        }


        [HttpPost("signUp")]
        public async Task<ActionResult> SignUp([FromBody] LoginModel loginModel)
        {
            UserDTO userDTO = await _userService.GetByEmail(loginModel.Email);
            if (userDTO != null)
                return StatusCode(400, "Account with such email already exists.");

            var userAdded = await _userService.SignUp(userDTO.Email, loginModel.Password);
            if (!userAdded.Succeeded)
                return StatusCode(401, "Wrong message");
            return Ok();
        }
    }
}
