using IFX_Entidades.Domain.Entities;
using IFX_Entidades.Infrastructure.ConfigurationMapping;
using Microsoft.EntityFrameworkCore;

namespace IFX_Entidades.Infrastructure.Persistence
{
	public class ProjectDbContext : DbContext
	{
		public DbSet<Entidad> Entidades { get; set; }
		public DbSet<Empleado> Empleados { get; set; }
		public DbSet<Usuario> Usuarios { get; set; }

        public ProjectDbContext(DbContextOptions<ProjectDbContext> dbContextOptions) : base(dbContextOptions) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new EntidadMapping());
			modelBuilder.ApplyConfiguration(new EmpleadoMapping());
			modelBuilder.ApplyConfiguration(new UsuarioMapping());

			modelBuilder.Entity<Entidad>().HasMany<Empleado>().WithOne(c => c.Entidad).HasForeignKey(c=> c.EntidadId).HasPrincipalKey(c=>c.Id);
		}


	}
}
