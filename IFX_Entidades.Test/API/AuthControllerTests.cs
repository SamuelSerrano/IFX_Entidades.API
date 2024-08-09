using IFX_Entidades.API.Controllers;
using IFX_Entidades.Application.Services.Interfaces;
using IFX_Entidades.Domain.Entities;
using IFX_Entidades.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace IFX_Entidades.Test.API
{
	public class AuthControllerTests
	{
		private readonly Mock<IUsuarioService> _usuarioServiceMock;
		private readonly Mock<IAuthService> _authServiceMock;
		private readonly AuthController _controller;

		public AuthControllerTests()
		{
			_usuarioServiceMock = new Mock<IUsuarioService>();
			_authServiceMock = new Mock<IAuthService>();
			_controller = new AuthController(_usuarioServiceMock.Object, _authServiceMock.Object);
		}

		[Fact]
		public async Task Login_ShouldReturnBadRequest_WhenModelStateIsInvalid()
		{
			// Arrange
			_controller.ModelState.AddModelError("key", "error");

			var loginModel = new LoginModel { Login = "testLogin", Password = "testPassword" };

			// Act
			var result = await _controller.Login(loginModel);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			Assert.Equal(400, badRequestResult.StatusCode);
		}

		[Fact]
		public async Task Login_ShouldReturnUnauthorized_WhenAuthenticationFails()
		{
			// Arrange
			_usuarioServiceMock.Setup(service => service.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()))
				.ReturnsAsync((Usuario)null);

			var loginModel = new LoginModel { Login = "testLogin", Password = "testPassword" };

			// Act
			var result = await _controller.Login(loginModel);

			// Assert
			var unauthorizedResult = Assert.IsType<UnauthorizedResult>(result);
			Assert.Equal(401, unauthorizedResult.StatusCode);
		}

		[Fact]
		public async Task Login_ShouldReturnOkAndToken_WhenAuthenticationSucceeds()
		{
			// Arrange
			var user = new Usuario { Login = "testLogin", Password = "testPassword" };
			var token = "generatedToken";

			_usuarioServiceMock.Setup(service => service.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()))
				.ReturnsAsync(user);
			_authServiceMock.Setup(service => service.GenerateJwtToken(It.IsAny<Usuario>()))
				.Returns(token);

			var loginModel = new LoginModel { Login = "testLogin", Password = "testPassword" };

			// Act
			var result = await _controller.Login(loginModel);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			OkObjectResult objectResult = (OkObjectResult)result;
			Assert.Equal(200, objectResult.StatusCode);
		}
	}
}
