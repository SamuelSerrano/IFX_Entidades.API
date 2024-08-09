using IFX_Entidades.Infrastructure.Persistence;
using IFX_Entidades.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IFX_Entidades.Infrastructure.Repositories
{
	public class GenericRepository<T> : IRepository<T> where T : class
	{
		private readonly ProjectDbContext _db;
		internal DbSet<T> dbSet;

        public GenericRepository(ProjectDbContext db)
        {
			_db = db;
			dbSet = _db.Set<T>();
		}

        public async Task AddAsync(T entity)
		{
			await dbSet.AddAsync(entity);
		}

		public virtual async Task<IEnumerable<T>> GetAllAsync()
		{
			return await dbSet.ToArrayAsync();
		}

		public virtual async Task<T> GetAsync(Expression<Func<T, bool>> filter)
		{
			IQueryable<T> query = dbSet;
			query = query.Where(filter);
			return await query.FirstOrDefaultAsync();
		}

		public void Remove(T entity)
		{
			dbSet.Remove(entity);
		}

		public void Update(T entity)
		{
			dbSet.Update(entity);
		}

		public async Task SaveAsync()
		{
			await _db.SaveChangesAsync();
		}
	}
}
