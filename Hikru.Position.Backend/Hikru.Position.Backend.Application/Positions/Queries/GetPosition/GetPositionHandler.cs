using Hikru.Position.Backend.Application.Exceptions;
using Hikru.Position.Backend.Application.Interfaces.Persistence;
using MediatR;

namespace Hikru.Position.Backend.Application.Positions.Queries.GetPosition
{
	public class GetPositionHandler : IRequestHandler<GetPositionQuery, GetPositionResult>
	{
		private readonly IUnitOfWork _uow;

		public GetPositionHandler(IUnitOfWork uow)
		{
			_uow = uow;
		}

		public async Task<GetPositionResult> Handle(GetPositionQuery request, CancellationToken cancellationToken)
		{
			var result = await _uow.Positions.GetByIdAsync(request.PositionId);

			if (result == null)
				throw new NotFoundException($"Position not found for Id: {request.PositionId}");

			return new GetPositionResult
			{
				PositionId = result.Id,
				Title = result.Title,
				Description = result.Description,
				Location = result.Location,
				Status = result.Status,
				RecruiterId = result.RecruiterId,
				Recruiter = $"{result.Recruiter.Name} ({result.Recruiter.Seniority})",
				DepartmentId = result.DepartmentId,
				Department = result.Department.Name,
				Budget = result.Budget,
				ClosingDate = result.ClosingDate
			};
		}
	}
}
