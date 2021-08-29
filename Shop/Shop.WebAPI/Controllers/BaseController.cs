using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Shop.WebAPI.Auth;

namespace Shop.WebAPI.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        protected BaseController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        protected async Task<string> GetCurrentUserId()
        {
            var token = await HttpContext.GetTokenAsync("Bearer", "access_token");
            var jwtToken = _authManager.DecodeJwtToken(token);
            if (jwtToken == null)
            {
                throw new Exception("Wrong Token.");
            }

            if (jwtToken.Claims == null)
            {
                throw new Exception("Wrong Token.");
            }

            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == "id").Value;

            return userId;
        }

    }
}