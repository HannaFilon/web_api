using Microsoft.AspNetCore.Identity;
using Shop.Business.Models;
using Shop.Business.ModelsDto;
using System.Threading.Tasks;

namespace Shop.Business.IServices
{
    public interface IUserService
    {
        public Task<IdentityResult> SignUp(string email, string password);
        public Task<bool> CheckPassword(string email, string password);
        public Task<UserDto> SignIn(string email, string password);
        public Task<UserDto> GetByEmail(string email);
        public Task<UserDto> GetById(string userId);
        public Task<UserDto> UpdateUser(string userId, UserModel userModel);
        public Task<IdentityResult> UpdatePassword(string userId, PasswordUpdateModel passwordUpdateModel);
    }
}