using Hikru.Position.Backend.Application.Interfaces.Persistence;
using MediatR;

namespace Hikru.Position.Backend.Application.Positions.Commands.CreatePosition
{
	public class CreatePositionHandler : IRequestHandler<CreatePositionCommand, CreatePositionResult>
	{
		private readonly IUnitOfWork _uow;

		public CreatePositionHandler(IUnitOfWork uow)
		{
			_uow = uow;
		}

		public async Task<CreatePositionResult> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
		{
			var position = new Domain.Entities.Position
			{
				Id = Guid.NewGuid(),
				Title = request.Title,
				Description = request.Description,
				Location = request.Location,
				Status = request.Status,
				RecruiterId = request.RecruiterId,
				DepartmentId = request.DepartmentId,
				Budget = request.Budget,
				ClosingDate = request.ClosingDate
			};

			await _uow.Positions.AddAsync(position);
			await _uow.SaveChangesAsync();

			var result = await _uow.Positions.GetByIdAsync(position.Id);

			return new CreatePositionResult
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
