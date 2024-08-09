using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IFX_Entidades.Infrastructure.Repositories.Interfaces
{
	public interface IRepository<T> where T : class
	{
		Task AddAsync(T entity);		
		void Update(T entity);
		void Remove(T entity);

		Task<T> GetAsync(Expression<Func<T, bool>> filter);
		Task<IEnumerable<T>> GetAllAsync();

		Task SaveAsync();

	}
}
