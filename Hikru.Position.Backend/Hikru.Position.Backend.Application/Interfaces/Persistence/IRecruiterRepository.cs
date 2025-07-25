using Hikru.Position.Backend.Domain.Entities;

namespace Hikru.Position.Backend.Application.Interfaces.Persistence
{
	public interface IRecruiterRepository
	{
		Task<Recruiter?> GetByIdAsync(Guid id);
		Task<List<Recruiter>> GetAllAsync();
		Task AddAsync(Recruiter recruiter);
		void Update(Recruiter recruiter);
		void Delete(Recruiter recruiter);
		Task<bool> ExistsAsync(Guid id);
	}
}
