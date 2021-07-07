using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shop.Business.Models;
using System.Threading.Tasks;

namespace Shop.Business.Implementation
{
    public class UserService: IUserService
    {
        private readonly UserManager<UserDTO> _userManager;
        private readonly SignInManager<UserDTO> _signInManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<UserDTO> userManager, SignInManager<UserDTO> signInManager, IMapper mapper) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        /*
        public Task<IdentityResult> SignUp(string email, string password) 
        {
        
        }
        
        public async Task<UserDTO> SignIn(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false,false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(email);
                var userDTO = _mapper.Map<UserDTO>(user);
                userDTO.Role = await GetUserRole(user);

                return userDTO;
            }
            else
                return null;
        }

        public async Task<string> GetUserRole(UserDTO userDTO)
        {

        }

        
        public async Task<bool> CheckPassword(string email, string password)
        { 
            var user = await _userManager.FindByEmailAsync(email);


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
        */
    }
}
