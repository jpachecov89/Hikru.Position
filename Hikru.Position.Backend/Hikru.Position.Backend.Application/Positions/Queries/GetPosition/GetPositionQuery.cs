using MediatR;

namespace Hikru.Position.Backend.Application.Positions.Queries.GetPosition
{
	public class GetPositionQuery : IRequest<GetPositionResult>
	{
		public Guid PositionId { get; set; }
	}
}
