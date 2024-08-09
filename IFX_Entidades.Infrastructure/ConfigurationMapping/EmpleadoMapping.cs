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
	public class EmpleadoMapping : IEntityTypeConfiguration<Empleado>
	{
		public void Configure(EntityTypeBuilder<Empleado> builder)
		{
			builder.ToTable("Empleado");

			builder.Property(p => p.Id).HasColumnType("INTEGER").IsRequired().ValueGeneratedOnAdd();
			builder.Property(p => p.Nombre).HasColumnType("TEXT").IsRequired();
			builder.Property(p => p.Cargo).HasColumnType("TEXT").IsRequired();
			builder.Property(p => p.EntidadId).HasColumnType("INTEGER").IsRequired();
			builder.HasKey(p => p.Id);

			builder.HasOne(e => e.Entidad)
				.WithMany(emp => emp.Empleados)
				.HasForeignKey(emp => emp.EntidadId);				

		}
	}
}
