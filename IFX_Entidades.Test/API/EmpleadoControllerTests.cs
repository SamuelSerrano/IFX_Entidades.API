using AutoMapper;
using IFX_Entidades.API.Controllers;
using IFX_Entidades.Application.Services.Interfaces;
using IFX_Entidades.Domain.Entities;
using IFX_Entidades.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFX_Entidades.Test.API
{
	public class EmpleadoControllerTests
	{
		private readonly EmpleadoController _controller;
		private readonly Mock<IEmpleadoService> _serviceMock;
		private readonly Mock<IMapper> _mapperMock;

		public EmpleadoControllerTests()
		{
			_serviceMock = new Mock<IEmpleadoService>();
			_mapperMock = new Mock<IMapper>();
			_controller = new EmpleadoController(_serviceMock.Object, _mapperMock.Object);
		}

		[Fact]
		public async Task GetAll_ShouldReturnOkResult_WhenEmpleadosAreFound()
		{
			// Arrange
			var empleados = new List<Empleado> { new Empleado { Id = 1, Nombre = "John" } };
			_serviceMock.Setup(service => service.GetAll()).ReturnsAsync(empleados);

			// Act
			var result = await _controller.GetAll();

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var returnEmpleados = Assert.IsAssignableFrom<IEnumerable<Empleado>>(okResult.Value);
			Assert.Equal(empleados.Count, returnEmpleados.Count());
		}

		[Fact]
		public async Task GetAll_ShouldReturnNotFound_WhenNoEmpleadosAreFound()
		{
			// Arrange
			_serviceMock.Setup(service => service.GetAll()).ThrowsAsync(new Exception("No hay empleados creados"));

			// Act
			var result = await _controller.GetAll();

			// Assert
			var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
			Assert.Equal("No hay empleados creados", notFoundResult.Value);
		}

		[Fact]
		public async Task GetById_ShouldReturnOkResult_WhenEmpleadoIsFound()
		{
			// Arrange
			var empleado = new Empleado { Id = 1, Nombre = "John" };
			_serviceMock.Setup(service => service.GetEmpleadoById(1)).ReturnsAsync(empleado);

			// Act
			var result = await _controller.GetById(1);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var returnEmpleado = Assert.IsType<Empleado>(okResult.Value);
			Assert.Equal(empleado.Id, returnEmpleado.Id);
		}

		[Fact]
		public async Task GetById_ShouldReturnNotFound_WhenEmpleadoIsNotFound()
		{
			// Arrange
			_serviceMock.Setup(service => service.GetEmpleadoById(1)).ThrowsAsync(new Exception("El empleado con id=1 no existe"));

			// Act
			var result = await _controller.GetById(1);

			// Assert
			var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
			Assert.Equal("El empleado con id=1 no existe", notFoundResult.Value);
		}

		[Fact]
		public async Task GetEmpleadoByIdEntidad_ShouldReturnOkResult_WhenEmpleadosAreFound()
		{
			// Arrange
			var empleados = new List<Empleado> { new Empleado { Id = 1, Nombre = "John" } };
			_serviceMock.Setup(service => service.GetEmpleadosByEntidad(1)).ReturnsAsync(empleados);

			// Act
			var result = await _controller.GetEmpleadoByIdEntidad(1);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var returnEmpleados = Assert.IsAssignableFrom<IEnumerable<Empleado>>(okResult.Value);
			Assert.Equal(empleados.Count, returnEmpleados.Count());
		}

		[Fact]
		public async Task GetEmpleadoByIdEntidad_ShouldReturnNotFound_WhenNoEmpleadosAreFound()
		{
			// Arrange
			_serviceMock.Setup(service => service.GetEmpleadosByEntidad(1)).ThrowsAsync(new Exception("No hay empleados para la entidad"));

			// Act
			var result = await _controller.GetEmpleadoByIdEntidad(1);

			// Assert
			var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
			Assert.Equal("No hay empleados para la entidad", notFoundResult.Value);
		}

		[Fact]
		public async Task Add_ShouldReturnCreatedAtAction_WhenEmpleadoIsAdded()
		{
			// Arrange
			var empleadoRequest = new EmpleadoRequest { Nombre = "John" };
			var empleado = new Empleado { Id = 1, Nombre = "John" };
			_mapperMock.Setup(m => m.Map<Empleado>(empleadoRequest)).Returns(empleado);
			_serviceMock.Setup(service => service.Add(empleado)).ReturnsAsync(empleado);

			// Act
			var result = await _controller.Add(empleadoRequest);

			// Assert
			var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
			var returnEmpleado = Assert.IsType<Empleado>(createdAtActionResult.Value);
			Assert.Equal(empleado.Id, returnEmpleado.Id);
		}

		[Fact]
		public async Task Add_ShouldReturnBadRequest_WhenExceptionIsThrown()
		{
			// Arrange
			var empleadoRequest = new EmpleadoRequest { Nombre = "John" };
			var empleado = new Empleado { Id = 1, Nombre = "John" };
			_mapperMock.Setup(m => m.Map<Empleado>(empleadoRequest)).Returns(empleado);
			_serviceMock.Setup(service => service.Add(empleado)).ThrowsAsync(new Exception("Error al agregar empleado"));

			// Act
			var result = await _controller.Add(empleadoRequest);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			Assert.Equal("Error al agregar empleado", badRequestResult.Value);
		}

		[Fact]
		public async Task Update_ShouldReturnOk_WhenEmpleadoIsUpdated()
		{
			// Arrange
			var empleadoRequest = new EmpleadoRequest { Nombre = "John" };
			var empleado = new Empleado { Id = 1, Nombre = "John" };
			_mapperMock.Setup(m => m.Map<Empleado>(empleadoRequest)).Returns(empleado);
			_serviceMock.Setup(service => service.Update(1, empleado)).Returns(Task.CompletedTask);

			// Act
			var result = await _controller.Update(1, empleadoRequest);

			// Assert
			Assert.IsType<OkResult>(result);
		}

		[Fact]
		public async Task Update_ShouldReturnNotFound_WhenExceptionIsThrown()
		{
			// Arrange
			var empleadoRequest = new EmpleadoRequest { Nombre = "John" };
			var empleado = new Empleado { Id = 1, Nombre = "John" };
			_mapperMock.Setup(m => m.Map<Empleado>(empleadoRequest)).Returns(empleado);
			_serviceMock.Setup(service => service.Update(1, empleado)).ThrowsAsync(new Exception("Empleado no encontrado"));

			// Act
			var result = await _controller.Update(1, empleadoRequest);

			// Assert
			var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
			Assert.Equal("Empleado no encontrado", notFoundResult.Value);
		}

		[Fact]
		public async Task Delete_ShouldReturnNoContent_WhenEmpleadoIsDeleted()
		{
			// Arrange
			_serviceMock.Setup(service => service.Delete(1)).Returns(Task.CompletedTask);

			// Act
			var result = await _controller.Delete(1);

			// Assert
			Assert.IsType<NoContentResult>(result);
		}

		[Fact]
		public async Task Delete_ShouldReturnNotFound_WhenExceptionIsThrown()
		{
			// Arrange
			_serviceMock.Setup(service => service.Delete(1)).ThrowsAsync(new Exception("Empleado no encontrado"));

			// Act
			var result = await _controller.Delete(1);

			// Assert
			var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
			Assert.Equal("Empleado no encontrado", notFoundResult.Value);
		}
	}
}
