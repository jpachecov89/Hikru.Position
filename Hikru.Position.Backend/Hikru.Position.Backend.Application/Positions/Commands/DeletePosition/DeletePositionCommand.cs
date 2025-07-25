using MediatR;

namespace Hikru.Position.Backend.Application.Positions.Commands.DeletePosition
{
	public class DeletePositionCommand : IRequest<DeletePositionResult>
	{
		public Guid PositionId { get; set; }
	}
}
