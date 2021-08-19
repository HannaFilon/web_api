using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shop.Business.IServices;
using Shop.Business.Models;
using Shop.Business.ModelsDto;
using Shop.DAL.Core.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Shop.Business.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, IMemoryCache cache)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _cache = cache;
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

            _cache.Set(userDto.Id, userDto);

            return resultCreating;
        }

        public async Task<UserDto> SignIn(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);
            if (!result.Succeeded)
            {
                return null;
            }

            if (_cache.TryGetValue(user.Id, out UserDto userDto))
            {
                return userDto;
            }

            user = await _userManager.FindByEmailAsync(email);
            userDto = _mapper.Map<UserDto>(user);
            userDto.Role = await GetUserRole(user);

            return userDto;
        }

        public async Task<bool> CheckPassword(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            var result = await _userManager.CheckPasswordAsync(user, password);

            return result;
        }

        public async Task<UserDto> GetByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return null;
            }

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Role = await GetUserRole(user);

            return userDto;
        }

        public async Task<UserDto> GetById(string userId)
        {
            if (_cache.TryGetValue(userId, out UserDto userDto))
            {
                return userDto;
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            userDto = _mapper.Map<UserDto>(user);
            userDto.Role = await GetUserRole(user);
            _cache.Set(userDto.Id, userDto);

            return userDto;
        }

        public async Task<UserDto> UpdateUser(string userId, UserModel userModel)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentNullException("User not found.");
            }

            _mapper.Map(userModel, user);
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                throw new Exception("Update failed.");
            }

            if (_cache.TryGetValue(userId, out UserDto userDto))
            {
                _cache.Remove(userId);
            }

            userDto = _mapper.Map<UserDto>(user);
            userDto.Role = await GetUserRole(user);
            _cache.Set(userDto.Id, userDto);

            return userDto;
        }

        public async Task<IdentityResult> UpdatePassword(string userId, PasswordUpdateModel passwordUpdateModel)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentNullException("User not found.");
            }

            var result = await
                _userManager.ChangePasswordAsync(user, passwordUpdateModel.Password, passwordUpdateModel.NewPassword);
            return result;
        }

        private async Task<string> GetUserRole(User user)
            => (await _userManager.GetRolesAsync(user))?.FirstOrDefault();
    }
}
