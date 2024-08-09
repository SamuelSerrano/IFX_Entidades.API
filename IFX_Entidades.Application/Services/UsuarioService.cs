using IFX_Entidades.Application.Services.Interfaces;
using IFX_Entidades.Domain.Entities;
using IFX_Entidades.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFX_Entidades.Application.Services
{
	public class UsuarioService : IUsuarioService
	{
		private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<Usuario> AuthenticateAsync(string login, string password)
		{
			var usuario = await _usuarioRepository.GetAsync(u => u.Login == login && u.Password == password);
			return usuario;
		}
	}
}
