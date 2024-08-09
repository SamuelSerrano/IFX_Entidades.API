using IFX_Entidades.Application.Services;
using IFX_Entidades.Domain.Entities;
using IFX_Entidades.Infrastructure.Repositories.Interfaces;
using Moq;
using System.Linq.Expressions;

namespace IFX_Entidades.Test.Application
{
	public class EmpleadoServiceTests
	{
		private readonly Mock<IEmpleadoRepository> _empleadoRepositoryMock;
		private readonly Mock<IEntidadRepository> _entidadRepositoryMock;
		private readonly EmpleadoService _empleadoService;

		public EmpleadoServiceTests()
		{
			_empleadoRepositoryMock = new Mock<IEmpleadoRepository>();
			_entidadRepositoryMock = new Mock<IEntidadRepository>();
			_empleadoService = new EmpleadoService(_empleadoRepositoryMock.Object, _entidadRepositoryMock.Object);
		}

		[Fact]
		public async Task Add_ShouldThrowException_WhenEmpleadoIsNull()
		{
			// Act & Assert
			await Assert.ThrowsAsync<Exception>(() => _empleadoService.Add(null));
		}

		[Fact]
		public async Task Add_ShouldThrowException_WhenEntidadDoesNotExist()
		{
			// Arrange
			var empleado = new Empleado { EntidadId = 1 };
			_entidadRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Entidad, bool>>>())).ReturnsAsync((Entidad)null);

			// Act & Assert
			await Assert.ThrowsAsync<Exception>(() => _empleadoService.Add(empleado));
		}

		[Fact]
		public async Task Add_ShouldCallAddAsync_WhenEmpleadoIsValid()
		{
			// Arrange
			var empleado = new Empleado { EntidadId = 1 };
			var entidad = new Entidad { Id = 1 };
			_entidadRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Entidad, bool>>>())).ReturnsAsync(entidad);

			// Act
			await _empleadoService.Add(empleado);

			// Assert
			_empleadoRepositoryMock.Verify(repo => repo.AddAsync(empleado), Times.Once);
			_empleadoRepositoryMock.Verify(repo => repo.SaveAsync(), Times.Once);
		}

		[Fact]
		public async Task Delete_ShouldThrowException_WhenEmpleadoDoesNotExist()
		{
			// Arrange
			_empleadoRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Empleado, bool>>>())).ReturnsAsync((Empleado)null);

			// Act & Assert
			await Assert.ThrowsAsync<Exception>(() => _empleadoService.Delete(1));
		}

		[Fact]
		public async Task Delete_ShouldCallRemove_WhenEmpleadoExists()
		{
			// Arrange
			var empleado = new Empleado { Id = 1 };
			_empleadoRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Empleado, bool>>>())).ReturnsAsync(empleado);

			// Act
			await _empleadoService.Delete(1);

			// Assert
			_empleadoRepositoryMock.Verify(repo => repo.Remove(empleado), Times.Once);
			_empleadoRepositoryMock.Verify(repo => repo.SaveAsync(), Times.Once);
		}

		[Fact]
		public async Task GetAll_ShouldThrowException_WhenNoEmpleadosExist()
		{
			// Arrange
			_empleadoRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(Enumerable.Empty<Empleado>());

			// Act & Assert
			await Assert.ThrowsAsync<Exception>(() => _empleadoService.GetAll());
		}

		[Fact]
		public async Task GetAll_ShouldReturnEmpleados_WhenEmpleadosExist()
		{
			// Arrange
			var empleados = new List<Empleado> { new Empleado { Id = 1 } };
			_empleadoRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(empleados);

			// Act
			var result = await _empleadoService.GetAll();

			// Assert
			Assert.NotNull(result);
			Assert.Single(result);
		}

		[Fact]
		public async Task GetEmpleadoById_ShouldThrowException_WhenEmpleadoDoesNotExist()
		{
			// Arrange
			_empleadoRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Empleado, bool>>>())).ReturnsAsync((Empleado)null);

			// Act & Assert
			await Assert.ThrowsAsync<Exception>(() => _empleadoService.GetEmpleadoById(1));
		}

		[Fact]
		public async Task GetEmpleadoById_ShouldReturnEmpleado_WhenEmpleadoExists()
		{
			// Arrange
			var empleado = new Empleado { Id = 1 };
			_empleadoRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Empleado, bool>>>())).ReturnsAsync(empleado);

			// Act
			var result = await _empleadoService.GetEmpleadoById(1);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(1, result.Id);
		}

		[Fact]
		public async Task Update_ShouldThrowException_WhenEmpleadoDoesNotExist()
		{
			// Arrange
			_empleadoRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Empleado, bool>>>())).
			ReturnsAsync((Empleado)null);

			// Act & Assert
			await Assert.ThrowsAsync<Exception>(() => _empleadoService.Update(1, new Empleado()));
		}

		[Fact]
		public async Task Update_ShouldThrowException_WhenEntidadDoesNotExist()
		{
			// Arrange
			var empleado = new Empleado { Id = 1, EntidadId = 2 };
			_empleadoRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Empleado, bool>>>())).ReturnsAsync(empleado);
			_entidadRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Entidad, bool>>>())).ReturnsAsync((Entidad)null);

			// Act & Assert
			await Assert.ThrowsAsync<Exception>(() => _empleadoService.Update(1, empleado));
		}

		[Fact]
		public async Task Update_ShouldCallUpdate_WhenEmpleadoIsValid()
		{
			// Arrange
			var empleado = new Empleado { Id = 1, EntidadId = 2 };
			var entidad = new Entidad { Id = 2 };
			_empleadoRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Empleado, bool>>>())).ReturnsAsync(empleado);
			_entidadRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Entidad, bool>>>())).ReturnsAsync(entidad);

			// Act
			await _empleadoService.Update(1, empleado);

			// Assert
			_empleadoRepositoryMock.Verify(repo => repo.Update(empleado), Times.Once);
			_empleadoRepositoryMock.Verify(repo => repo.SaveAsync(), Times.Once);
		}
	}
}
