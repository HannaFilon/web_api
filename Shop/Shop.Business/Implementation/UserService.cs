using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shop.Business.IServices;
using Shop.Business.Models;
using Shop.DAL.Core.Entities;
using System;
using System.Threading.Tasks;


namespace Shop.Business.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> SignUp(string email, string password)
        {
            var userDTO = new UserDTO
            {
                UserName = email,
                Email = email,
                Role = "User",
            };

            var user = _mapper.Map<User>(userDTO);
            var resultCreating = await _userManager.CreateAsync(user, password);

            if (!resultCreating.Succeeded)
                throw new Exception("The user can not be signed up.");


            userDTO.Id = await _userManager.GetUserIdAsync(user);

            var resultAdding = await _userManager.AddToRoleAsync(user, userDTO.Role);
            if (!resultAdding.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                throw new Exception("The user role can not be added.");
            }

            return resultCreating;
        }

        public async Task<UserDTO> SignIn(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(email);
                var userDTO = _mapper.Map<UserDTO>(user);
                userDTO.Role = await GetUserRole(userDTO);

                return userDTO;
            }
            else
                return null;
        }

        public async Task<bool> CheckPassword(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var result = await _userManager.CheckPasswordAsync(user, password);
            return result;
        }

        public async Task<UserDTO> GetByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<string> GetUserRole(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);

            var roles = await _userManager.GetRolesAsync(user);
            return roles[0];
        }
    }
}
