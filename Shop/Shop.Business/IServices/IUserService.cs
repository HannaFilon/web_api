using Microsoft.AspNetCore.Identity;
using Shop.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Business.IServices
{
    public interface IUserService
    {
        public Task<IdentityResult> SignUp(string email, string password);
        public Task<bool> CheckPassword(string email, string password);
        public Task<UserDTO> SignIn(string email, string password);
        public Task<UserDTO> GetByEmail(string email);
        public Task<string> GetUserRole(UserDTO userDTO);
        public Task Logout();
    }
}