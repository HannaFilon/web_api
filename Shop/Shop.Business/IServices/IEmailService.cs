using Microsoft.AspNetCore.Identity;
using Shop.Business.Models;
using System;
using System.Threading.Tasks;

namespace Shop.Business.IServices
{
    public interface IEmailService
    {
        public Task SendEmailConfirmMessage(string email, string callback);
        public Task<IdentityResult> ConfirmEmail(string email, string code);
        public Task<string> GenerateEmailConfirmationToken(UserDTO userDTO);
    }
}
