using Hikru.Position.Backend.Application.Interfaces.Persistence;
using Hikru.Position.Backend.Domain.Entities;
using Hikru.Position.Backend.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Hikru.Position.Backend.Infrastructure.Persistence.Repositories
{
	public class RecruiterRepository : IRecruiterRepository
	{
		private readonly AppDbContext _context;

		public RecruiterRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<Recruiter?> GetByIdAsync(Guid id) =>
			await _context.Recruiters.FindAsync(id);

		public async Task<List<Recruiter>> GetAllAsync() =>
			await _context.Recruiters.ToListAsync();

		public async Task AddAsync(Recruiter recruiter) =>
			await _context.Recruiters.AddAsync(recruiter);

		public void Update(Recruiter recruiter) =>
			_context.Recruiters.Update(recruiter);

		public void Delete(Recruiter recruiter) =>
			_context.Recruiters.Remove(recruiter);

		public async Task<bool> ExistsAsync(Guid id) =>
			await _context.Recruiters.AnyAsync(x => x.Id == id);
	}
}
