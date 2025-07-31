using Hikru.Position.Backend.Application.Exceptions;
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
			if (string.IsNullOrWhiteSpace(request.Title))
				throw new BadRequestException("Title cannot be empty. Review again");
			if (string.IsNullOrWhiteSpace(request.Description))
				throw new BadRequestException("Description cannot be empty. Review again");
			if (string.IsNullOrWhiteSpace(request.Location))
				throw new BadRequestException("Location cannot be empty. Review again");
			if (request.Budget <= 0)
				throw new BadRequestException("Invalid amount for Budget. Review again");

			var position = new Domain.Entities.Position
			(
				request.Title,
				request.Description,
				request.Location,
				request.Status,
				request.RecruiterId,
				request.DepartmentId,
				request.Budget,
				request.ClosingDate
			);

			await _uow.Positions.AddAsync(position);
			await _uow.SaveChangesAsync();

			var result = await _uow.Positions.GetByIdAsync(position.Id);

			if (result == null)
				throw new BadRequestException("Position cannot be created. Review again");

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
