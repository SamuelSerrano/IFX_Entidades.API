using IFX_Entidades.Application.Services.Interfaces;
using IFX_Entidades.Domain.Entities;
using IFX_Entidades.Domain.Models;
using IFX_Entidades.Infrastructure.Repositories.Interfaces;

namespace IFX_Entidades.Application.Services
{
	public class EmpleadoService : IEmpleadoService
	{
		private readonly IEmpleadoRepository _empleadoRepository;
		private readonly IEntidadRepository _entidadRepository;
		public EmpleadoService(IEmpleadoRepository empleadoRepository, IEntidadRepository entidadRepository)
        {
            _empleadoRepository = empleadoRepository;
			_entidadRepository = entidadRepository;
        }

		public async Task<Empleado> Add(Empleado empleado)
		{
			if (empleado == null) throw new Exception("Empleado no está definido");

			Entidad entidad = await _entidadRepository.GetAsync(e => e.Id == empleado.EntidadId);
			if (entidad == null) throw new Exception($"La entidad con id = {empleado.EntidadId} no existe");

			await _empleadoRepository.AddAsync(empleado);
			await _empleadoRepository.SaveAsync();
			return empleado;
		}

		public async Task Delete(int id)
		{
			Empleado empleado = await _empleadoRepository.GetAsync(e => e.Id == id);
			if (empleado == null) throw new Exception($"El empleado con id={id} no existe");

			_empleadoRepository.Remove(empleado);
			await _empleadoRepository.SaveAsync();
		}

		public async Task<IEnumerable<Empleado>> GetAll()
		{
			IEnumerable<Empleado> empleados = await _empleadoRepository.GetAllAsync();
			if (empleados.Count() == 0) throw new Exception("No hay empleados creados");			
			return empleados;
		}

		public async Task<Empleado> GetEmpleadoById(int id)
		{
			Empleado empleado = await _empleadoRepository.GetAsync(e => e.Id == id);
			if (empleado == null) throw new Exception($"El empleado con id={id} no existe");
			return empleado;
		}

		public async Task<List<Empleado>> GetEmpleadosByEntidad(int idEntidad)
		{
			Entidad entidad = await _entidadRepository.GetAsync(e=>e.Id == idEntidad);
			if (entidad == null) throw new Exception($"La entidad con id = {idEntidad} no existe");
            
            List<Empleado> empleados = await _empleadoRepository.GetAllEmpleadosAsync(e => e.EntidadId == idEntidad);
			if (empleados.Count == 0) throw new Exception($"No hay empleados para la entidad: {entidad.Nombre}");
			return empleados;
		}

		public async Task Update(int id, Empleado nuevoEmpleado)
		{
			Empleado empleado = await _empleadoRepository.GetAsync(e => e.Id == id);
			if (empleado == null) throw new Exception($"El empleado con id={id} no existe");

			if (nuevoEmpleado.EntidadId != 0) 
			{
				Entidad entidad = await _entidadRepository.GetAsync(e => e.Id == nuevoEmpleado.EntidadId);
				if (entidad == null) throw new Exception($"La entidad con id = {nuevoEmpleado.EntidadId} no existe");
			}
			

			var entityType = typeof(Empleado);
			var properties = entityType.GetProperties();
			foreach (var property in properties)
			{
				var newValue = property.GetValue(nuevoEmpleado);
				var defaultValue = property.PropertyType.IsValueType ? Activator.CreateInstance(property.PropertyType) : null;

				if (newValue != null && !newValue.Equals(defaultValue)) property.SetValue(empleado, newValue);
			}

			_empleadoRepository.Update(empleado);
			await _empleadoRepository.SaveAsync();
		}
	}
}
