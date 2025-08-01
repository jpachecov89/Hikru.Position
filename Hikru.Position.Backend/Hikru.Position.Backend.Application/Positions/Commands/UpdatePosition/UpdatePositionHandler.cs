﻿using Hikru.Position.Backend.Application.Exceptions;
using Hikru.Position.Backend.Application.Interfaces.Persistence;
using MediatR;

namespace Hikru.Position.Backend.Application.Positions.Commands.UpdatePosition
{
	public class UpdatePositionHandler : IRequestHandler<UpdatePositionCommand, UpdatePositionResult>
	{
		private readonly IUnitOfWork _uow;

		public UpdatePositionHandler(IUnitOfWork uow)
		{
			_uow = uow;
		}

		public async Task<UpdatePositionResult> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
		{
			if (string.IsNullOrWhiteSpace(request.Title))
				throw new BadRequestException("Title cannot be empty. Review again");
			if (string.IsNullOrWhiteSpace(request.Description))
				throw new BadRequestException("Description cannot be empty. Review again");
			if (string.IsNullOrWhiteSpace(request.Location))
				throw new BadRequestException("Location cannot be empty. Review again");
			if (request.Budget <= 0)
				throw new BadRequestException("Invalid amount for Budget. Review again");

			var position = await _uow.Positions.GetByIdAsync(request.PositionId);

			if (position == null)
				throw new NotFoundException($"Position not found for Id: {request.PositionId}");

			position.Title = request.Title;
			position.Description = request.Description;
			position.Location = request.Location;
			position.Status = request.Status;
			position.RecruiterId = request.RecruiterId;
			position.DepartmentId = request.DepartmentId;
			position.Budget = request.Budget;
			position.ClosingDate = request.ClosingDate;

			_uow.Positions.Update(position);
			await _uow.SaveChangesAsync();

			var result = await _uow.Positions.GetByIdAsync(position.Id);

			if (result == null)
				throw new BadRequestException("Position cannot be updated. Review again");

			return new UpdatePositionResult
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
