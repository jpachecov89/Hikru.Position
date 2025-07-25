using Hikru.Position.Backend.Application.Interfaces.Persistence;
using Hikru.Position.Backend.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Hikru.Position.Backend.Infrastructure.Persistence.Repositories
{
	public class PositionRepository : IPositionRepository
	{
		private readonly AppDbContext _context;

		public PositionRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<Domain.Entities.Position?> GetByIdAsync(Guid id) =>
			await _context.Positions
			.Include(x => x.Recruiter)
			.Include(x => x.Department)
			.FirstOrDefaultAsync(x => x.Id == id);

		public async Task<List<Domain.Entities.Position>> GetAllAsync() =>
			await _context.Positions
			.Include(x => x.Recruiter)
			.Include(x => x.Department)
			.ToListAsync();

		public async Task AddAsync(Domain.Entities.Position position) =>
			await _context.Positions.AddAsync(position);

		public void Update(Domain.Entities.Position position) =>
			_context.Positions.Update(position);

		public void Delete(Domain.Entities.Position position) =>
			_context.Positions.Remove(position);

		public async Task<bool> ExistsAsync(Guid id) =>
			await _context.Positions.AnyAsync(x => x.Id  == id);
	}
}
