using System.ComponentModel.DataAnnotations;

namespace IFX_Entidades.Domain.Models
{
	public class EmpleadoRequest
	{
		public string? Nombre { get; set; }
		public string? Cargo { get; set; }
		public int EntidadId { get; set; }
	}
}
