using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IMapper _mapper;
        private const string TokenIdKey = "TokenId";
        private const string JwtKey = "611bd2ba-c254-4a07-9308-27cb74cd5237";

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }


        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserModel userModel)
        {
            var token = HttpContext.Request.Headers["TokenId"];
            var userId = ValidateJwtToken(token);
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("This method is unavailable.");
            }

            var userDto = await _userService.UpdateUser(userId, userModel);
            userModel = _mapper.Map<UserModel>(userDto);

            return Ok(userModel);
        }

        [HttpPost("password")]
        public async Task<IActionResult> UpdatePassword([FromBody] PasswordUpdateModel passwordUpdateModel)
        {
            var token = HttpContext.Request.Headers["TokenId"];
            var userId = ValidateJwtToken(token);
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
            var token = HttpContext.Request.Headers["TokenId"];
            var userId = ValidateJwtToken(token);
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("This method is unavailable.");
            }

            var userDto = await _userService.GetById(userId);
            if (userDto == null)
            {
                return Unauthorized("User not found");
            }

            var userModel = _mapper.Map<UserModel>(userDto);

            return Ok(userModel);
        }

        public string ValidateJwtToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new SecurityTokenException("Invalid token");

            var key = "611bd2ba-c254-4a07-9308-27cb74cd5237";
            try
            {

                var principal = new JwtSecurityTokenHandler()
                    .ValidateToken(token,
                        new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                        },
                        out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

                return userId;
            }
            catch
            {
                return null;
            }
        }
    }
}
