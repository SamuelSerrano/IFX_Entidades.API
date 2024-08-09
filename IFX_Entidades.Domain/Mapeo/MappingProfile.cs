using AutoMapper;
using IFX_Entidades.Domain.Entities;
using IFX_Entidades.Domain.Models;

namespace IFX_Entidades.Domain.Mapeo
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<EntidadRequest, Entidad>();
            CreateMap<EmpleadoRequest, Empleado>();
		}
    }
}
