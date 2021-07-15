using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shop.Business.IServices;
using Shop.Business.Models;
using Shop.DAL.Core.Entities;
using System;
using System.Linq;
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
            var userDto = new UserDto
            {
                UserName = email,
                Email = email,
                Role = "User"
            };

            var user = _mapper.Map<User>(userDto);
            var resultCreating = await _userManager.CreateAsync(user, password);
            if (!resultCreating.Succeeded)
            {
                throw new Exception("User can not be signed up.");
            }

            userDto.Id = await _userManager.GetUserIdAsync(user);

            var resultAdding = await _userManager.AddToRoleAsync(user, userDto.Role);
            if (!resultAdding.Succeeded)
            {
                await _userManager.DeleteAsync(user);

                throw new Exception("User role can not be added.");
            }

            return resultCreating;
        }

        public async Task<UserDto> SignIn(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(email);
                var userDto = _mapper.Map<UserDto>(user);
                userDto.Role = await GetUserRole(user);

                return userDto;
            }
            
            return null;
        }

        public async Task<bool> CheckPassword(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.CheckPasswordAsync(user, password);

            return result;
        }

        public async Task<UserDto> GetByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Role = await GetUserRole(user);

            return userDto;
        }

        private async Task<string> GetUserRole(User user)
            => (await _userManager.GetRolesAsync(user))?.FirstOrDefault();
    }
}
