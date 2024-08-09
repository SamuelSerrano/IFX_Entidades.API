using System.ComponentModel.DataAnnotations;

namespace IFX_Entidades.Domain.Models
{
	public class LoginModel
	{
		[Required(ErrorMessage = "El campo Login es obligatorio.")]
		public string? Login { get; set; }
		[Required(ErrorMessage = "El campo Password es obligatorio.")]
		public string? Password { get; set; }
	}
}
