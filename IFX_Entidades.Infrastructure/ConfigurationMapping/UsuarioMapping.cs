using IFX_Entidades.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFX_Entidades.Infrastructure.ConfigurationMapping
{
	internal class UsuarioMapping : IEntityTypeConfiguration<Usuario>
	{
		public void Configure(EntityTypeBuilder<Usuario> builder)
		{
			builder.ToTable("Usuario");
			builder.Property(p => p.Id).HasColumnType("INTEGER").IsRequired().ValueGeneratedOnAdd();
			builder.Property(p => p.Login).HasColumnType("TEXT").IsRequired();
			builder.Property(p => p.Password).HasColumnType("TEXT").IsRequired();
			builder.Property(p => p.Rol).HasColumnType("INTEGER").IsRequired();

			builder.HasKey(p => p.Id);

			builder.HasData(new Usuario[]
				{ 
					new Usuario
					{ 
						Id = 1,
						Login = "Usuario1",
						Password = "ClaveFacil",
						Rol = 2
					},
					new Usuario 
					{ 
						Id = 2,
						Login = "Admin1",
						Password = "ClaveAdminFacil",
						Rol = 1
					}
				}
				);


		}
	}
}
