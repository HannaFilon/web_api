using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Shop.Business.ModelsDto;

namespace Shop.Business.IServices
{
    public interface IEmailService
    {
        public Task SendEmailConfirmMessage(string email, string callback);
        public Task<IdentityResult> ConfirmEmail(string email, string code);
        public Task<string> GenerateEmailConfirmationToken(UserDto userDto);
    }
}
