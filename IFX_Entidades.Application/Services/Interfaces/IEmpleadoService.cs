using IFX_Entidades.Domain.Entities;

namespace IFX_Entidades.Application.Services.Interfaces
{
	public interface IEmpleadoService
	{
		Task<Empleado> Add(Empleado empleado);
		Task Update(int id, Empleado nuevoEmpleado);
		Task Delete(int id);
		Task<IEnumerable<Empleado>> GetAll();
		Task<Empleado> GetEmpleadoById(int id);
		Task<List<Empleado>> GetEmpleadosByEntidad(int idEntidad);
	}
}
