namespace Hikru.Position.Backend.Application.Interfaces.Persistence
{
	public interface IUnitOfWork
	{
		IDepartmentRepository Departments { get; }
		IRecruiterRepository Recruiters { get; }
		IPositionRepository Positions { get; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
