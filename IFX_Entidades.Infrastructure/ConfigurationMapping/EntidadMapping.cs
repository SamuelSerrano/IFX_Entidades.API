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
	public class EntidadMapping : IEntityTypeConfiguration<Entidad>
	{
		public void Configure(EntityTypeBuilder<Entidad> builder)
		{
			builder.ToTable("Entidad");

			builder.Property(p => p.Id).HasColumnType("INTEGER").IsRequired().ValueGeneratedOnAdd();
			builder.Property(p => p.Nombre).HasColumnType("TEXT").IsRequired();
			builder.Property(p => p.Sector).HasColumnType("TEXT").IsRequired();

			builder.HasKey(p => p.Id);
		}
	}
}
