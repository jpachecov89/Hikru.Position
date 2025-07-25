using Hikru.Position.Backend.Domain.Enums;
using MediatR;

namespace Hikru.Position.Backend.Application.Positions.Commands.UpdatePosition
{
	public class UpdatePositionCommand : IRequest<UpdatePositionResult>
	{
		public Guid PositionId { get; set; }
		public string Title { get; set; } = null!;
		public string Description { get; set; } = null!;
		public string Location { get; set; } = null!;
		public PositionStatus Status { get; set; }
		public Guid RecruiterId { get; set; }
		public Guid DepartmentId { get; set; }
		public double Budget { get; set; }
		public DateTime? ClosingDate { get; set; }
	}
}
