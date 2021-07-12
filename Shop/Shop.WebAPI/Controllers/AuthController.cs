using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Threading.Tasks;
using Shop.Business.IServices;
using Shop.Business.Models;
using System;

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
            UserDTO userDTO = await _userService.GetByEmail(loginModel.Email);
            if (userDTO == null)
                return StatusCode(401, "Wrong email. Try again");
            bool isCorrect = await _userService.CheckPassword(userDTO.Email, loginModel.Password);
            if (!isCorrect)
                return StatusCode(401, "Wrong password. Try again");
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("signUp")]
        public async Task<ActionResult> SignUp([FromBody] LoginModel loginModel)
        {
            UserDTO userDTO = await _userService.GetByEmail(loginModel.Email);
            if (userDTO != null)
                return StatusCode(400, "Account with such email already exists.");

            var userAdded = await _userService.SignUp(loginModel.Email, loginModel.Password);
            if (!userAdded.Succeeded)
            {
                return StatusCode(401, "The user can not be signed up. Try registering once again.");
            }

            userDTO = await _userService.GetByEmail(loginModel.Email);
            var code = await _emailService.GenerateEmailConfirmationToken(userDTO);
            var callback = Url.Action(nameof(ConfirmEmail), "Auth", new { id = userDTO.Id, code }, Request.Scheme);
            await _emailService.SendEmailConfirmMessage(userDTO.Email, callback);
            
            return Ok();
        }

       
        
        [HttpGet("confirmEmail", Name = "ConfirmEmailRoute")]
        public async Task<ActionResult> ConfirmEmail(string email, string code)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(code))
            {
                 return BadRequest("");
            }

            var emailConfirmed = await _emailService.ConfirmEmail(email, code);

            if (emailConfirmed.Succeeded) 
            {
                return Ok("");
            }
            return BadRequest("");
        }
    }
}
