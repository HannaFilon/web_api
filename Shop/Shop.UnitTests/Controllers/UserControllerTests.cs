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
using System.Collections.Generic;

namespace Shop.UnitTests.Controllers
{
    public class UserControllerTests
    {
        private static readonly UserController _userController;
        private static readonly IAuthManager _authManager;

        static UserControllerTests()
        {
            _authManager = A.Fake<IAuthManager>();

            var fakeUserService = A.Fake<IUserService>();
            var fakeMapper = A.Fake<IMapper>();
            var fakeServiceProvider = A.Fake<IServiceProvider>();
            var fakeAuthService = A.Fake<IAuthenticationService>();
            var fakeTicket = A.Fake<AuthenticationTicket>();
            var fakeContext = A.Fake<HttpContext>();

            A.CallTo(() => fakeUserService.UpdateUser(A<string>._, A<UserModel>._))
                 .Returns(new UserDto());
            A.CallTo(() => fakeUserService.UpdatePassword(A<string>._, A<PasswordUpdateModel>._))
                .Returns(Task.FromResult(IdentityResult.Success));
            A.CallTo(() => fakeMapper.Map<UserModel>(A<UserDto>._))
                .Returns(new UserModel());
            A.CallTo(() => _authManager.DecodeJwtToken(A<string>._))
                .Returns(new JwtSecurityToken());
            A.CallTo(() => fakeContext.RequestServices)
                .Returns(fakeServiceProvider);
            A.CallTo(() => fakeServiceProvider.GetService(typeof(IAuthenticationService)))
                .Returns(fakeAuthService);
            A.CallTo(() => fakeAuthService.AuthenticateAsync(fakeContext, A<string>._))
                .Returns(AuthenticateResult.Success(fakeTicket));

            _userController = new UserController(fakeUserService, fakeMapper, _authManager)
            {
                ControllerContext = { HttpContext = fakeContext }
            };
        }

        [Fact]
        public async void UpdateUser_ReturnUpdatedModel()
        {
            StubAuthManager(Guid.NewGuid().ToString());

            var result = await _userController.UpdateUser(new UserModel());

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.IsType<UserModel>(okResult.Value);
            Assert.NotNull(okResult.Value);
        }

        [Theory]
        [MemberData(nameof(UpdateUser_ReturnsBadRequestTestData))]
        public async void UpdateUser_ReturnsBadRequest(string userId, UserModel userModel, string expectedError)
        {
            StubAuthManager(userId);

            var result = await _userController.UpdateUser(userModel);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.IsType<string>(badRequestResult.Value);
            Assert.Equal(expectedError, badRequestResult.Value);
        }

        [Fact]
        public async void UpdatePassword_Return204()
        {
            StubAuthManager(Guid.NewGuid().ToString());

            var result = await _userController.UpdatePassword(new PasswordUpdateModel());

            var statusCode = result as StatusCodeResult;
            Assert.NotNull(statusCode);
            Assert.Equal(204, statusCode.StatusCode);
        }

        [Theory]
        [MemberData(nameof(UpdatePassword_ReturnsBadRequestTestData))]
        public async void UpdatePassword_ReturnsBadRequest(string userId, PasswordUpdateModel passwordUpdateModel, string expectedError)
        {
            StubAuthManager(userId);

            var result = await _userController.UpdatePassword(passwordUpdateModel);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.IsType<string>(badRequestResult.Value);
            Assert.Equal(expectedError, badRequestResult.Value);
        }

        [Fact]
        public async void GetUserInfo_ReturnsUserModel()
        {
            StubAuthManager(Guid.NewGuid().ToString());

            var result = await _userController.GetUserInfo();

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.IsType<UserModel>(okResult.Value);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async void GetUserInfo_ReturnsBadRequest()
        {
            StubAuthManager(string.Empty);

            var result = await _userController.GetUserInfo();

            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.IsType<string>(badRequestResult.Value);
            Assert.Equal("This method is unavailable.", badRequestResult.Value);
        }


        #region Helpers

        public static IEnumerable<object[]> UpdateUser_ReturnsBadRequestTestData
            => new[]
            {
                new object[] { string.Empty, new UserModel(), "This method is unavailable." },
                new object[] { Guid.NewGuid().ToString(), null, "Not enough parameters to update user." }
            };

        public static IEnumerable<object[]> UpdatePassword_ReturnsBadRequestTestData
            => new[]
            {
                new object[] { string.Empty, new PasswordUpdateModel(), "This method is unavailable." },
                new object[] { Guid.NewGuid().ToString(), null, "Not enough parameters to update password." }
            };

        private void StubAuthManager(string userId)
        {
            var jwtToken = new JwtSecurityToken(claims: new Claim[]
            {
                new("id", userId)
            });

            A.CallTo(() => _authManager.DecodeJwtToken(A<string>._))
                .Returns(jwtToken);
        }

        #endregion
    }
}