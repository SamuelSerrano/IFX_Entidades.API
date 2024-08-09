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
	public class EntidadControllerTests
	{
		private readonly Mock<IEntidadService> _entidadServiceMock;
		private readonly Mock<IMapper> _mapperMock;
		private readonly EntidadController _controller;

		public EntidadControllerTests()
		{
			_entidadServiceMock = new Mock<IEntidadService>();
			_mapperMock = new Mock<IMapper>();
			_controller = new EntidadController(_entidadServiceMock.Object, _mapperMock.Object);
		}

		[Fact]
		public async Task GetAll_ShouldReturnOk_WhenEntitiesExist()
		{
			// Arrange
			var entidades = new List<Entidad>
		{
			new Entidad { Id = 1, Nombre = "Entidad1" },
			new Entidad { Id = 2, Nombre = "Entidad2" }
		};
			_entidadServiceMock.Setup(service => service.GetAll())
				.ReturnsAsync(entidades);

			// Act
			var result = await _controller.GetAll();

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var returnValue = Assert.IsType<List<Entidad>>(okResult.Value);
			Assert.Equal(entidades.Count, returnValue.Count);
		}

		[Fact]
		public async Task GetById_ShouldReturnOk_WhenEntityExists()
		{
			// Arrange
			var entidad = new Entidad { Id = 1, Nombre = "Entidad1" };
			_entidadServiceMock.Setup(service => service.GetEntidadById(1))
				.ReturnsAsync(entidad);

			// Act
			var result = await _controller.GetById(1);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var returnValue = Assert.IsType<Entidad>(okResult.Value);
			Assert.Equal(entidad.Id, returnValue.Id);
		}

		[Fact]
		public async Task GetById_ShouldReturnNotFound_WhenEntityDoesNotExist()
		{
			// Arrange
			_entidadServiceMock.Setup(service => service.GetEntidadById(1))
				.ThrowsAsync(new Exception("La entidad con id=1 no existe"));

			// Act
			var result = await _controller.GetById(1);

			// Assert
			var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
			Assert.Equal(404, notFoundResult.StatusCode);
		}

		[Fact]
		public async Task Add_ShouldReturnCreatedAtAction_WhenEntityIsAdded()
		{
			// Arrange
			var entidadRequest = new EntidadRequest { Nombre = "Nueva Entidad" };
			var entidad = new Entidad { Id = 1, Nombre = "Nueva Entidad" };
			_mapperMock.Setup(m => m.Map<Entidad>(entidadRequest))
				.Returns(entidad);
			_entidadServiceMock.Setup(service => service.Add(entidad))
				.ReturnsAsync(entidad);

			// Act
			var result = await _controller.Add(entidadRequest);

			// Assert
			var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
			var returnValue = Assert.IsType<Entidad>(createdAtActionResult.Value);
			Assert.Equal(entidad.Id, returnValue.Id);
		}

		[Fact]
		public async Task Update_ShouldReturnOk_WhenEntityIsUpdated()
		{
			// Arrange
			var entidadRequest = new EntidadRequest { Nombre = "Entidad Actualizada" };
			var entidad = new Entidad { Id = 1, Nombre = "Entidad Antiguo" };
			_mapperMock.Setup(m => m.Map<Entidad>(entidadRequest))
				.Returns(entidad);
			_entidadServiceMock.Setup(service => service.Update(1, entidad))
				.Returns(Task.CompletedTask);

			// Act
			var result = await _controller.Update(1, entidadRequest);

			// Assert
			var okResult = Assert.IsType<OkResult>(result);
			Assert.Equal(200, okResult.StatusCode);
		}

		[Fact]
		public async Task Delete_ShouldReturnNoContent_WhenEntityIsDeleted()
		{
			// Arrange
			_entidadServiceMock.Setup(service => service.Delete(1))
				.Returns(Task.CompletedTask);

			// Act
			var result = await _controller.Delete(1);

			// Assert
			var noContentResult = Assert.IsType<NoContentResult>(result);
			Assert.Equal(204, noContentResult.StatusCode);
		}

		[Fact]
		public async Task Delete_ShouldReturnNotFound_WhenEntityDoesNotExist()
		{
			// Arrange
			_entidadServiceMock.Setup(service => service.Delete(1))
				.ThrowsAsync(new Exception("La entidad con id=1 no existe"));

			// Act
			var result = await _controller.Delete(1);

			// Assert
			var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
			Assert.Equal(404, notFoundResult.StatusCode);
		}
	}
}
