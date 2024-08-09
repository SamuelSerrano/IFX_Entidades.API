namespace IFX_Entidades.Domain.Entities
{
	public class Empleado
	{
		public int Id { get; set; }
		public string? Nombre { get; set; }
		public string? Cargo { get; set; }
		public int EntidadId { get; set; }
		public Entidad? Entidad { get; set; }
	}
}
