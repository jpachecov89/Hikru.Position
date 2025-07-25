using Hikru.Position.Backend.Application.Interfaces.Persistence;
using MediatR;

namespace Hikru.Position.Backend.Application.Positions.Commands.DeletePosition
{
	public class DeletePositionHandler : IRequestHandler<DeletePositionCommand, DeletePositionResult>
	{
		private readonly IUnitOfWork _uow;

		public DeletePositionHandler(IUnitOfWork uow)
		{
			_uow = uow;
		}

		public async Task<DeletePositionResult> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
		{
			var position = await _uow.Positions.GetByIdAsync(request.PositionId);

			_uow.Positions.Delete(position);
			await _uow.SaveChangesAsync();

			return new DeletePositionResult { IsDeleted = true };
		}
	}
}
