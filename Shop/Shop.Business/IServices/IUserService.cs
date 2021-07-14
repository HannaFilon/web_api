using Microsoft.AspNetCore.Identity;
using Shop.Business.Models;
using System.Threading.Tasks;

namespace Shop.Business.IServices
{
    public interface IUserService
    {
        public Task<IdentityResult> SignUp(string email, string password);
        public Task<bool> CheckPassword(string email, string password);
        public Task<UserDto> SignIn(string email, string password);
        public Task<UserDto> GetByEmail(string email);
        public Task<string> GetUserRole(UserDto userDto);
        public Task Logout();
    }
}