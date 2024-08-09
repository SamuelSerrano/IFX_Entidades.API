using IFX_Entidades.Application.Services;
using IFX_Entidades.Domain.Entities;
using IFX_Entidades.Infrastructure.Repositories.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IFX_Entidades.Test.Application
{
	public class UsuarioServiceTests
	{
		private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
		private readonly UsuarioService _usuarioService;

		public UsuarioServiceTests()
		{
			_usuarioRepositoryMock = new Mock<IUsuarioRepository>();
			_usuarioService = new UsuarioService(_usuarioRepositoryMock.Object);
		}

		[Fact]
		public async Task AuthenticateAsync_ShouldReturnNull_WhenNoMatchingUserFound()
		{
			// Arrange
			_usuarioRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Usuario, bool>>>()))
				.ReturnsAsync((Usuario)null);

			// Act
			var result = await _usuarioService.AuthenticateAsync("invalidLogin", "invalidPassword");

			// Assert
			Assert.Null(result);
		}

		[Fact]
		public async Task AuthenticateAsync_ShouldReturnUser_WhenMatchingUserFound()
		{
			// Arrange
			var expectedUser = new Usuario { Login = "validLogin", Password = "validPassword" };
			_usuarioRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Usuario, bool>>>()))
				.ReturnsAsync(expectedUser);

			// Act
			var result = await _usuarioService.AuthenticateAsync("validLogin", "validPassword");

			// Assert
			Assert.NotNull(result);
			Assert.Equal(expectedUser.Login, result.Login);
			Assert.Equal(expectedUser.Password, result.Password);
		}

		
	}
}
