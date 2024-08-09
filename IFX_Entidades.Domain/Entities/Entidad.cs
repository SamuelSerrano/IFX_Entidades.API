namespace IFX_Entidades.Domain.Entities
{
	public class Entidad
	{
		public int Id { get; set; }
		public string? Nombre { get; set; }
		public string? Sector { get; set; }
		public virtual ICollection<Empleado>? Empleados { get; set; }
        
    }
}
