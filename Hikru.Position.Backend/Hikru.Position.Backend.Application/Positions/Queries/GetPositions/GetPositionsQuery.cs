using MediatR;

namespace Hikru.Position.Backend.Application.Positions.Queries.GetPositions
{
	public class GetPositionsQuery : IRequest<List<GetPositionsResult>>
	{
	}
}
