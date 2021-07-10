using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shop.Business.IServices;
using Shop.Business.Models;
using Shop.DAL.Core.Entities;
using System.Threading.Tasks;

namespace Shop.Business.Implementation
{
    public class UserService: IUserService
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
                Email = email,
                Role = "User",
            };

            var user = _mapper.Map<User>(userDTO);
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded) {
                await _userManager.AddToRoleAsync(user, userDTO.Role);
            }

            return result; 
        }
        
        public async Task<UserDTO> SignIn(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false,false);

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

            return true;
        }        

        public async Task<UserDTO> GetByEmail(string email) 
        {
            var user = await _userManager.FindByEmailAsync(email);
            var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }
    
        public async Task Logout() 
        { 

        
        }

        public async Task<string> GetUserRole(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);

            var roles = await _userManager.GetRolesAsync(user);
            return roles[0];
        }
    }
}
