using Hikru.Position.Backend.Application.Interfaces.Persistence;
using Hikru.Position.Backend.Infrastructure.Persistence.Contexts;

namespace Hikru.Position.Backend.Infrastructure.Persistence
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _context;

		public IDepartmentRepository Departments { get; }
		public IRecruiterRepository Recruiters { get; }
		public IPositionRepository Positions { get; }

		public UnitOfWork(
			AppDbContext context,
			IDepartmentRepository departmentRepository,
			IRecruiterRepository recruiterRepository,
			IPositionRepository positionRepository)
		{
			_context = context;
			Departments = departmentRepository;
			Recruiters = recruiterRepository;
			Positions = positionRepository;
		}

		public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return await _context.SaveChangesAsync(cancellationToken);
		}
	}
}
