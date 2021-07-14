using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shop.Business.IServices;
using Shop.Business.Models;
using Shop.DAL.Core.Entities;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Shop.Business.Implementation
{
    public class EmailService: IEmailService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;


        public EmailService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
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
                throw new Exception();

            if (callback == null)
                throw new SmtpException("The url is null.");

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential("annfilon16", "password"),
                EnableSsl = true,
                Timeout = 120000
            };
            var message = new MailMessage
            {
                IsBodyHtml = true,
                Subject = "Email confirmation",
                Body = "Please confirm your account by clicking this link: "+
                       "<a href=\"" + callback + "\">link</a>",
                From = new MailAddress("annfilon16@gmail.com"),
            };
            message.To.Add(new MailAddress(email));
            smtp.Send(message);
        }

        public async Task<string> GenerateEmailConfirmationToken(UserDto userDto)
        {
            var user = await _userManager.FindByEmailAsync(userDto.Email);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return code;
        }
    }
}
