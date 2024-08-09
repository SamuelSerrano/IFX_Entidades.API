using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFX_Entidades.Domain.Entities
{
	public class Usuario
	{
		public int Id { get; set; }
		public string? Login { get; set; }
		public string? Password { get; set; }
		public int Rol { get; set; } 
	}
}
