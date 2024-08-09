using IFX_Entidades.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IFX_Entidades.Infrastructure.Repositories.Interfaces
{
	public interface IEmpleadoRepository: IRepository<Empleado>
	{
		Task<List<Empleado>> GetAllEmpleadosAsync(Expression<Func<Empleado, bool>> filter);
	}
}
