using IFX_Entidades.Domain.Entities;
using IFX_Entidades.Infrastructure.Persistence;
using IFX_Entidades.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IFX_Entidades.Infrastructure.Repositories
{
	public class EntidadRepository : GenericRepository<Entidad>, IEntidadRepository 
	{
		private readonly ProjectDbContext _context;
		public EntidadRepository(ProjectDbContext context) : base(context)
		{
			_context = context;
		}

		public override async Task<Entidad> GetAsync(Expression<Func<Entidad, bool>> filter)
		{
			IQueryable<Entidad> query = dbSet;
			query = query.Where(filter)
				.Include(emp => emp.Empleados);
			return await query.FirstOrDefaultAsync();
		}

		public override async Task<IEnumerable<Entidad>> GetAllAsync()
		{
			return await _context.Set<Entidad>().Include(emp=>emp.Empleados).ToArrayAsync();
		}
	}
}
