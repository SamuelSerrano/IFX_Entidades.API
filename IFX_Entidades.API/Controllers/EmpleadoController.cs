using AutoMapper;
using IFX_Entidades.Application.Services.Interfaces;
using IFX_Entidades.Domain.Entities;
using IFX_Entidades.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IFX_Entidades.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmpleadoController : ControllerBase
	{
		private readonly IEmpleadoService _service;
		private readonly IMapper _mapper;
		public EmpleadoController(IEmpleadoService service, IMapper mapper)
		{
			_service = service;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var empleados = await _service.GetAll();
				return Ok(empleados);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			try
			{
				var empleado = await _service.GetEmpleadoById(id);
				return Ok(empleado);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpGet("entidad/{idEntidad}")]
		public async Task<IActionResult> GetEmpleadoByIdEntidad(int idEntidad)
		{
			try
			{
				var empleados = await _service.GetEmpleadosByEntidad(idEntidad);
				return Ok(empleados);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpPost]
		[Authorize(Policy = "AdminPolicy")]
		public async Task<IActionResult> Add([FromBody] EmpleadoRequest empleadoRequest)
		{
			try
			{
				Empleado empleado = _mapper.Map<Empleado>(empleadoRequest);
				var result = await _service.Add(empleado);
				return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "AdminPolicy")]
		public async Task<IActionResult> Update(int id, [FromBody] EmpleadoRequest empleadoRequest)
		{
			try
			{
				Empleado empleado = _mapper.Map<Empleado>(empleadoRequest);
				await _service.Update(id, empleado);
				return Ok();
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		[Authorize(Policy = "AdminPolicy")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				await _service.Delete(id);
				return NoContent();
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}
	}
}
