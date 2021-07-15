using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Shop.Business.IServices;
using Shop.Business.Models;

namespace Shop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;


        public AuthController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }


        [AllowAnonymous]
        [HttpPost("signIn")]
        public async Task<ActionResult> SignIn([FromBody] LoginModel loginModel)
        {
            var userDto = await _userService.SignIn(loginModel.Email, loginModel.Password);
            if (userDto == null)
            {
                return StatusCode(401, "Wrong email or password. Try again");
            }
            
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("signUp")]
        public async Task<ActionResult> SignUp([FromBody] LoginModel loginModel)
        {
            var userDto = await _userService.GetByEmail(loginModel.Email);
            if (userDto != null)
            {
                return StatusCode(400, "Account with such email already exists.");
            }

            var userAdded = await _userService.SignUp(loginModel.Email, loginModel.Password);
            if (!userAdded.Succeeded)
            {
                return StatusCode(400, "User can not be signed up. Try registering once again.");
            }

            userDto = await _userService.GetByEmail(loginModel.Email);
            var code = await _emailService.GenerateEmailConfirmationToken(userDto);
            var callback = Url.Action(nameof(ConfirmEmail), "Auth", new {email = userDto.Email, code = code},
                Request.Scheme);
            await _emailService.SendEmailConfirmMessage(userDto.Email, callback);

            return new StatusCodeResult(201);
        }

        [AllowAnonymous]
        [HttpGet("confirmEmail", Name = "ConfirmEmailRoute")]
        public async Task<ActionResult> ConfirmEmail(string email, string code)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(code))
            {
                 return BadRequest("Wrong email or token.");
            }

            var emailConfirmed = await _emailService.ConfirmEmail(email, code);
            if (emailConfirmed.Succeeded) 
            {
                return Ok();
            }

            return BadRequest("The email wasn't confirmed.");
        }
    }
}
