using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Xunit;
using FakeItEasy;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Business.IServices;
using Shop.Business.Models;
using Shop.Business.ModelsDto;
using Shop.WebAPI.Auth;
using Shop.WebAPI.Controllers;

namespace Shop.UnitTests.Controllers
{
    public class UserControllerTests
    {
        private UserController SetUserController(string userId)
        {
            var fakeUserService = A.Fake<IUserService>();
            var fakeAuthManager = A.Fake<IAuthManager>();
            var fakeMapper = A.Fake<IMapper>();
            var fakeServiceProvider = A.Fake<IServiceProvider>();
            var fakeAuthService = A.Fake<IAuthenticationService>();
            var fakeTicket = A.Fake<AuthenticationTicket>();
            var fakeContext = A.Fake<HttpContext>();

            var jwtToken = new JwtSecurityToken(claims: new Claim[]
            {
                new("id", userId)
            });

            A.CallTo(() => fakeUserService.UpdateUser(A<string>._, A<UserModel>._))
                 .Returns(new UserDto());
            A.CallTo(() => fakeUserService.UpdatePassword(A<string>._, A<PasswordUpdateModel>._))
                .Returns(Task.FromResult(IdentityResult.Success));
            A.CallTo(() => fakeMapper.Map<UserModel>(A<UserDto>._))
                .Returns(new UserModel());
            A.CallTo(() => fakeAuthManager.DecodeJwtToken(A<string>._))
                .Returns(jwtToken);
            A.CallTo(() => fakeContext.RequestServices)
                .Returns(fakeServiceProvider);
            A.CallTo(() => fakeServiceProvider.GetService(typeof(IAuthenticationService)))
                .Returns(fakeAuthService);
            A.CallTo(() => fakeAuthService.AuthenticateAsync(fakeContext, A<string>._))
                .Returns(AuthenticateResult.Success(fakeTicket));

            var userController = new UserController(fakeUserService, fakeMapper, fakeAuthManager)
            {
                ControllerContext = { HttpContext = fakeContext }
            };

            return userController;
        }


        [Fact]
        public async void UpdateUser_ReturnUpdatedModel()
        {
            var userController = SetUserController(Guid.NewGuid().ToString());

            var result = await userController.UpdateUser(new UserModel());

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            var modelResult = okResult.Value as UserModel;
            Assert.NotNull(modelResult);
        }

        [Fact]
        public async void UpdateUser_ReturnsBadRequest_WrongToken()
        {
            var userController = SetUserController(string.Empty);

            var result = await userController.UpdateUser(new UserModel());

            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            var stringValue = badRequestResult.Value as string;
            Assert.Equal("This method is unavailable.", stringValue);
        }

        [Fact]
        public async void UpdateUser_ReturnsBadRequest_NullUserModel()
        {
            var userController = SetUserController(Guid.NewGuid().ToString());

            var result = await userController.UpdateUser(null);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            var stringValue = badRequestResult.Value as string;
            Assert.Equal("Not enough parameters to update user.", stringValue);
        }

        [Fact]
        public async void UpdatePassword_Return204()
        {
            var userController = SetUserController(Guid.NewGuid().ToString());

            var result = await userController.UpdatePassword(new PasswordUpdateModel());

            var statusCode = result as StatusCodeResult;
            Assert.NotNull(statusCode);
            Assert.Equal(204, statusCode.StatusCode);
        }

        [Fact]
        public async void UpdatePassword_ReturnsBadRequest_WrongToken()
        {
            var userController = SetUserController(string.Empty);

            var result = await userController.UpdatePassword(new PasswordUpdateModel());

            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            var stringValue = badRequestResult.Value as string;
            Assert.Equal("This method is unavailable.", stringValue);
        }

        [Fact]
        public async void UpdatePassword_ReturnsBadRequest_NullPasswordUpdateModel()
        {
            var userController = SetUserController(Guid.NewGuid().ToString());

            var result = await userController.UpdatePassword(null);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            var stringValue = badRequestResult.Value as string;
            Assert.Equal("Not enough parameters to update password.", stringValue);
        }

        [Fact]
        public async void GetUserInfo_ReturnsUserModel()
        {
            var userController = SetUserController(Guid.NewGuid().ToString());

            var result = await userController.GetUserInfo();

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            var modelResult = okResult.Value as UserModel;
            Assert.NotNull(modelResult);
        }

        [Fact]
        public async void GetUserInfo_ReturnsBadRequest_WrongToken()
        {
            var userController = SetUserController(string.Empty);

            var result = await userController.GetUserInfo();

            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            var stringValue = badRequestResult.Value as string;
            Assert.Equal("This method is unavailable.", stringValue);
        }
    }
}