using Hikru.Position.Backend.Application.Interfaces.Persistence;
using MediatR;

namespace Hikru.Position.Backend.Application.Positions.Queries.GetPositions
{
	public class GetPositionsHandler : IRequestHandler<GetPositionsQuery, List<GetPositionsResult>>
	{
		private readonly IUnitOfWork _uow;

		public GetPositionsHandler(IUnitOfWork uow)
		{
			_uow = uow;
		}

		public async Task<List<GetPositionsResult>> Handle(GetPositionsQuery request, CancellationToken cancellationToken)
		{
			var positions = await _uow.Positions.GetAllAsync();

			return positions.Select(x => new GetPositionsResult
			{
				PositionId = x.Id,
				Title = x.Title,
				Description = x.Description,
				Location = x.Location,
				Status = x.Status,
				RecruiterId = x.RecruiterId,
				Recruiter = $"{x.Recruiter.Name} ({x.Recruiter.Seniority})",
				DepartmentId = x.DepartmentId,
				Department = x.Department.Name,
				Budget = x.Budget,
				ClosingDate = x.ClosingDate
			}).ToList();
		}
	}
}
