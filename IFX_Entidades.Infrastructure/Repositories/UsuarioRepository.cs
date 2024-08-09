using IFX_Entidades.Domain.Entities;
using IFX_Entidades.Infrastructure.Persistence;
using IFX_Entidades.Infrastructure.Repositories.Interfaces;

namespace IFX_Entidades.Infrastructure.Repositories
{
	public class UsuarioRepository: GenericRepository<Usuario>, IUsuarioRepository
	{
		private readonly ProjectDbContext _context;
		public UsuarioRepository(ProjectDbContext context) : base(context)
		{
			_context = context;
		}
	}
}
