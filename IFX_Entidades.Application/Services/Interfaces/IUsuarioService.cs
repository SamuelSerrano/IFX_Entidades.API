using IFX_Entidades.Domain.Entities;

namespace IFX_Entidades.Application.Services.Interfaces
{
	public interface IUsuarioService
	{
		Task<Usuario> AuthenticateAsync(string login, string password);
	}
}
