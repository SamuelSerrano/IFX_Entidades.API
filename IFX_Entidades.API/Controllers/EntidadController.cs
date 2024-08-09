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
	public class EntidadController : ControllerBase
	{
		private readonly IEntidadService _entidadService;
		private readonly IMapper _mapper;
        public EntidadController(IEntidadService entidadService, IMapper mapper)
        {
            _entidadService = entidadService;
			_mapper = mapper;
        }

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var entidades = await _entidadService.GetAll();
				return Ok(entidades);
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
				var entidad = await _entidadService.GetEntidadById(id);
				return Ok(entidad);
			}
			catch (Exception ex) 
			{ 
				return NotFound(ex.Message);
			}			
		}

		[HttpPost]
		[Authorize(Policy = "AdminPolicy")]
		public async Task<IActionResult> Add([FromBody] EntidadRequest entidadRequest)
		{
			try
			{
				Entidad entidad = _mapper.Map<Entidad>(entidadRequest);
				await _entidadService.Add(entidad);
				return CreatedAtAction(nameof(GetById), new { id = entidad.Id }, entidad);
			}
			catch (Exception ex) { 
				return BadRequest(ex.Message);
			}			
		}

		[HttpPut("{id}")]
		[Authorize(Policy = "AdminPolicy")]
		public async Task<IActionResult> Update(int id, [FromBody] EntidadRequest entidadRequest)
		{
			try
			{
				Entidad entidad = _mapper.Map<Entidad>(entidadRequest);
				await _entidadService.Update(id, entidad);
				return Ok();
			}
			catch (Exception ex) { 
				return NotFound(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		[Authorize(Policy = "AdminPolicy")]
		public async Task<IActionResult> Delete(int id)
		{
			try { 
				await _entidadService.Delete(id);
				return NoContent();
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}
	}
}
