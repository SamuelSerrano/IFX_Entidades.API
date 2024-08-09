using IFX_Entidades.Domain.Entities;

namespace IFX_Entidades.Application.Services.Interfaces
{
	public interface IEntidadService
	{
		Task<Entidad> GetEntidadById(int id);
		Task<IEnumerable<Entidad>> GetAll();
		Task<Entidad> Add(Entidad entity);
		Task Update(int id, Entidad entity);
		Task Delete(int id);
	}
}
