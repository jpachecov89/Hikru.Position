using entity = Hikru.Position.Backend.Domain.Entities;

namespace Hikru.Position.Backend.Application.Interfaces.Persistence
{
	public interface IPositionRepository
	{
		Task<entity.Position?> GetByIdAsync(Guid id);
		Task<List<entity.Position>> GetAllAsync();
		Task AddAsync(entity.Position position);
		void Update(entity.Position position);
		void Delete(entity.Position position);
		Task<bool> ExistsAsync(Guid id);
	}
}
