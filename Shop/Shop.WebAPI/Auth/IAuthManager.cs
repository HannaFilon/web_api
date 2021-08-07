using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Shop.Business.ModelsDto;

namespace Shop.WebAPI.Auth
{
    public interface IAuthManager
    {
        public string GenerateToken(UserDto userDto, DateTime now);
        public JwtSecurityToken DecodeJwtToken(string token);
    }
}