using Hikru.Position.Backend.Domain.Entities;

namespace Hikru.Position.Backend.Application.Interfaces.Persistence
{
	public interface IDepartmentRepository
	{
		Task<Department?> GetByIdAsync(Guid id);
		Task<List<Department>> GetAllAsync();
		Task AddAsync(Department department);
		void Update(Department department);
		void Delete(Department department);
		Task<bool> ExistsAsync(Guid id);
	}
}
