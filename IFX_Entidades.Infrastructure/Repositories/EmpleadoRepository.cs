using IFX_Entidades.Domain.Entities;
using IFX_Entidades.Infrastructure.Persistence;
using IFX_Entidades.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IFX_Entidades.Infrastructure.Repositories
{
	public class EmpleadoRepository : GenericRepository<Empleado>, IEmpleadoRepository
	{
		private readonly ProjectDbContext _context;
		public EmpleadoRepository(ProjectDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<List<Empleado>> GetAllEmpleadosAsync(Expression<Func<Empleado, bool>> filter)
		{
			IQueryable<Empleado> query = dbSet;
			query = query.Where(filter).Include(ent=>ent.Entidad);
			return await query.ToListAsync();
		}

		public override async Task<Empleado> GetAsync(Expression<Func<Empleado, bool>> filter)
		{
			IQueryable<Empleado> query = dbSet;
			query = query.Where(filter)
				.Include(ent => ent.Entidad);
			return await query.FirstOrDefaultAsync();
		}

		public override async Task<IEnumerable<Empleado>> GetAllAsync()
		{
			return await _context.Set<Empleado>().Include(ent => ent.Entidad).ToArrayAsync();
		}
	}
}
