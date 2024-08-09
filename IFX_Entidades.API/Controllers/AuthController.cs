using IFX_Entidades.Application.Services.Interfaces;
using IFX_Entidades.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace IFX_Entidades.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IUsuarioService _usuarioService;
		private readonly IAuthService _authenticationService;
        public AuthController(IUsuarioService usuarioService, IAuthService authService)
        {
            _authenticationService = authService;
			_usuarioService = usuarioService;
        }

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
		{
			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			var usuario = await _usuarioService.AuthenticateAsync(loginModel.Login, loginModel.Password);
			if (usuario == null) return Unauthorized();
			var token = _authenticationService.GenerateJwtToken(usuario);
			return Ok(new { Token = token });
		}
	}
}
