using IFX_Entidades.Application.Services.Interfaces;
using IFX_Entidades.Domain.Entities;
using IFX_Entidades.Domain.Models;
using IFX_Entidades.Infrastructure.Repositories.Interfaces;

namespace IFX_Entidades.Application.Services
{
	public class EntidadService : IEntidadService
	{
		private readonly IEntidadRepository _entidadRepository;

        public EntidadService(IEntidadRepository entidadRepository)
        {
            _entidadRepository = entidadRepository;
        }
        public async Task<Entidad> Add(Entidad entidad)
		{
			if (entidad == null) throw new Exception("La entidad no está definida");
			await _entidadRepository.AddAsync(entidad);
			await _entidadRepository.SaveAsync();

			return entidad;
		}

		public async Task Delete(int id)
		{
			Entidad entidad = await _entidadRepository.GetAsync(e=>e.Id == id);
			if (entidad == null) throw new Exception($"La entidad con id={id} no existe");

			_entidadRepository.Remove(entidad);
			await _entidadRepository.SaveAsync();
		}

		public async Task<IEnumerable<Entidad>> GetAll()
		{
			IEnumerable<Entidad> entidades = await _entidadRepository.GetAllAsync();
			if (entidades.Count() == 0) throw new Exception("No hay entidades creadas");			
			return entidades;
		}

		public async Task<Entidad> GetEntidadById(int id)
		{
			Entidad entidad = await _entidadRepository.GetAsync(e=>e.Id==id);
			if (entidad == null) throw new Exception($"La entidad con id={id} no existe");			
			return entidad;
		}

		public async Task Update(int id, Entidad nuevaEntidad)
		{
			Entidad entidad = await _entidadRepository.GetAsync(e => e.Id == id);
			if (entidad == null) throw new Exception($"La entidad con id={id} no existe");

			nuevaEntidad.Id = entidad.Id;
			var entityType = typeof(Entidad);
			var properties = entityType.GetProperties();
			foreach (var property in properties)
			{
				var newValue = property.GetValue(nuevaEntidad);
				var defaultValue = property.PropertyType.IsValueType ? Activator.CreateInstance(property.PropertyType) : null;

				if (newValue != null && !newValue.Equals(defaultValue)) property.SetValue(entidad, newValue);
			}

			_entidadRepository.Update(entidad);
			await _entidadRepository.SaveAsync();
		}
	}

}
