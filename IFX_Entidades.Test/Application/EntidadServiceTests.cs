using IFX_Entidades.Application.Services;
using IFX_Entidades.Domain.Entities;
using IFX_Entidades.Infrastructure.Repositories.Interfaces;
using Moq;
using System.Linq.Expressions;

namespace IFX_Entidades.Test.Application
{
	public class EntidadServiceTests
	{
		private readonly Mock<IEntidadRepository> _entidadRepositoryMock;
		private readonly EntidadService _entidadService;

		public EntidadServiceTests()
		{
			_entidadRepositoryMock = new Mock<IEntidadRepository>();
			_entidadService = new EntidadService(_entidadRepositoryMock.Object);
		}

		[Fact]
		public async Task Add_ShouldThrowException_WhenEntidadIsNull()
		{
			// Act & Assert
			await Assert.ThrowsAsync<Exception>(() => _entidadService.Add(null));
		}

		[Fact]
		public async Task Add_ShouldCallAddAsync_WhenEntidadIsValid()
		{
			// Arrange
			var entidad = new Entidad { Nombre = "Entidad Test", Sector = "Sector Test" };

			// Act
			await _entidadService.Add(entidad);

			// Assert
			_entidadRepositoryMock.Verify(repo => repo.AddAsync(entidad), Times.Once);
			_entidadRepositoryMock.Verify(repo => repo.SaveAsync(), Times.Once);
		}

		[Fact]
		public async Task Delete_ShouldThrowException_WhenEntidadDoesNotExist()
		{
			// Arrange
			_entidadRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Entidad, bool>>>())).ReturnsAsync((Entidad)null);

			// Act & Assert
			await Assert.ThrowsAsync<Exception>(() => _entidadService.Delete(1));
		}

		[Fact]
		public async Task Delete_ShouldCallRemove_WhenEntidadExists()
		{
			// Arrange
			var entidad = new Entidad { Id = 1 };
			_entidadRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Entidad, bool>>>())).ReturnsAsync(entidad);

			// Act
			await _entidadService.Delete(1);

			// Assert
			_entidadRepositoryMock.Verify(repo => repo.Remove(entidad), Times.Once);
			_entidadRepositoryMock.Verify(repo => repo.SaveAsync(), Times.Once);
		}

		[Fact]
		public async Task GetAll_ShouldThrowException_WhenNoEntidadesExist()
		{
			// Arrange
			_entidadRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(Enumerable.Empty<Entidad>());

			// Act & Assert
			await Assert.ThrowsAsync<Exception>(() => _entidadService.GetAll());
		}

		[Fact]
		public async Task GetAll_ShouldReturnEntidades_WhenEntidadesExist()
		{
			// Arrange
			var entidades = new List<Entidad> { new Entidad { Id = 1, Nombre = "Entidad Test", Sector = "Sector Test" } };
			_entidadRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(entidades);

			// Act
			var result = await _entidadService.GetAll();

			// Assert
			Assert.NotNull(result);
			Assert.Single(result);
		}

		[Fact]
		public async Task GetEntidadById_ShouldThrowException_WhenEntidadDoesNotExist()
		{
			// Arrange
			_entidadRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Entidad, bool>>>())).ReturnsAsync((Entidad)null);

			// Act & Assert
			await Assert.ThrowsAsync<Exception>(() => _entidadService.GetEntidadById(1));
		}

		[Fact]
		public async Task GetEntidadById_ShouldReturnEntidad_WhenEntidadExists()
		{
			// Arrange
			var entidad = new Entidad { Id = 1, Nombre = "Entidad Test", Sector = "Sector Test" };
			_entidadRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Entidad, bool>>>())).ReturnsAsync(entidad);

			// Act
			var result = await _entidadService.GetEntidadById(1);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(1, result.Id);
		}

		[Fact]
		public async Task Update_ShouldThrowException_WhenEntidadDoesNotExist()
		{
			// Arrange
			_entidadRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Entidad, bool>>>())).ReturnsAsync((Entidad)null);

			// Act & Assert
			await Assert.ThrowsAsync<Exception>(() => _entidadService.Update(1, new Entidad()));
		}

		[Fact]
		public async Task Update_ShouldCallUpdate_WhenEntidadIsValid()
		{
			// Arrange
			var entidad = new Entidad { Id = 1, Nombre = "Entidad Original", Sector = "Sector Original" };
			var nuevaEntidad = new Entidad { Nombre = "Entidad Modificada", Sector = "Sector Modificado" };

			_entidadRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Entidad, bool>>>())).ReturnsAsync(entidad);

			// Act
			await _entidadService.Update(1, nuevaEntidad);

			// Assert
			_entidadRepositoryMock.Verify(repo => repo.Update(entidad), Times.Once);
			_entidadRepositoryMock.Verify(repo => repo.SaveAsync(), Times.Once);
		}
	}
}
