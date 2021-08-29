using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Shop.Business.IServices;
using Shop.DAL.Core.Entities;
using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Shop.Business.ModelsDto;

namespace Shop.Business.Implementation
{
    public class EmailService: IEmailService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly SmtpClient _smtpClient;
        private readonly IConfiguration _config;

        public EmailService(UserManager<User> userManager, IMapper mapper, SmtpClient smtpClient, IConfiguration config)
        {
            _userManager = userManager;
            _mapper = mapper;
            _smtpClient = smtpClient;
            _config = config;
        }

        public async Task<IdentityResult> ConfirmEmail(string email, string code)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.ConfirmEmailAsync(user, code);

            return result;
        }

        public async Task SendEmailConfirmMessage(string email, string callback)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (callback == null)
            {
                throw new ArgumentNullException("Url is null.");
            }

            var senderEmail = _config.GetSection("Email:Smtp:SenderEmail");
            var message = new MailMessage
            {
                IsBodyHtml = true,
                Subject = "Email confirmation",
                Body = $"Please confirm your account by clicking this link: <a href=\"{callback}\">link</a>",
                From = new MailAddress(senderEmail.Value),
            };

            message.To.Add(new MailAddress(email));
            await _smtpClient.SendMailAsync(message);
        }

        public async Task<string> GenerateEmailConfirmationToken(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return code;
        }
    }
}
