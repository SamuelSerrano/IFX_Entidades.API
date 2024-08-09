using IFX_Entidades.Application.Services;
using IFX_Entidades.Application.Services.Interfaces;
using IFX_Entidades.Domain.Entities;
using IFX_Entidades.Domain.Mapeo;
using IFX_Entidades.Infrastructure.Persistence;
using IFX_Entidades.Infrastructure.Repositories;
using IFX_Entidades.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IFX_Entidades.API.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
		{
			#region Authentication & Authorization
			var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});

			services.AddAuthorization(options =>
			{
				options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
			});


			#endregion

			#region Application
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IUsuarioService, UsuarioService>();
			services.AddScoped<IEntidadService, EntidadService>();
			services.AddScoped<IEmpleadoService, EmpleadoService>();
			#endregion

			#region Dominio
			services.AddAutoMapper(typeof(MappingProfile));
			#endregion

			#region Ïnfraestructura
			string connectionString = configuration.GetConnectionString("cnSqlite");
			services.AddDbContext<ProjectDbContext>(options =>
				options.UseSqlite(connectionString, b => b.MigrationsAssembly("IFX_Entidades.API")));

			services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
			services.AddScoped<IEntidadRepository, EntidadRepository>();
			services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();
			services.AddScoped<IUsuarioRepository, UsuarioRepository>();
			#endregion



			return services;
		}
	}
}
