using IFX_Entidades.Domain.Entities;

namespace IFX_Entidades.Application.Services.Interfaces
{
	public interface IAuthService
	{
		string GenerateJwtToken(Usuario usuario);
	}
}
