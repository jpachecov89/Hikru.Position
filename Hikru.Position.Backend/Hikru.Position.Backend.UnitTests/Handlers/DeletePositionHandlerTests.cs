using Hikru.Position.Backend.Application.Exceptions;
using Hikru.Position.Backend.Application.Interfaces.Persistence;
using Hikru.Position.Backend.Application.Positions.Commands.DeletePosition;
using Hikru.Position.Backend.Domain.Enums;
using Moq;

namespace Hikru.Position.Backend.UnitTests.Handlers
{
	public class DeletePositionHandlerTests
	{
		private readonly Mock<IUnitOfWork> _uowMock = new();

		private readonly DeletePositionHandler _handler;

		public DeletePositionHandlerTests()
		{
			_handler = new DeletePositionHandler(_uowMock.Object);
		}

		[Fact]
		public async Task Handle_ValidRequest_DeletesAndReturnsTrue()
		{
			var command = GetValidCommand();
			var position = GetPositionFromCommand(command);

			_uowMock.Setup(x => x.Positions.GetByIdAsync(command.PositionId))
				.ReturnsAsync(position);
			_uowMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.ReturnsAsync(1);

			var result = await _handler.Handle(command, CancellationToken.None);

			Assert.True(result.IsDeleted);
		}

		[Fact]
		public async Task Handle_ValidRequest_CallsDeleteAndSaveChanges()
		{
			var command = GetValidCommand();
			var position = GetPositionFromCommand(command);

			_uowMock.Setup(x => x.Positions.GetByIdAsync(command.PositionId))
				.ReturnsAsync(position);
			_uowMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.ReturnsAsync(1);

			await _handler.Handle(command, CancellationToken.None);

			_uowMock.Verify(x => x.Positions.Delete(It.IsAny<Domain.Entities.Position>()), Times.Once);
			_uowMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
		}

		[Fact]
		public async Task Handle_PositionNotFound_ThrowsNotFoundException()
		{
			var command = GetValidCommand();

			_uowMock.Setup(x => x.Positions.GetByIdAsync(command.PositionId))
				.ReturnsAsync((Domain.Entities.Position)null!);

			await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
		}

		private DeletePositionCommand GetValidCommand() => new()
		{
			PositionId = Guid.NewGuid()
		};

		private Domain.Entities.Position GetPositionFromCommand(DeletePositionCommand cmd)
		{
			Domain.Entities.Position position = new(
				"Dev",
				"Delete backend",
				"Remote",
				PositionStatus.Open,
				Guid.NewGuid(),
				Guid.NewGuid(),
				10000,
				DateTime.UtcNow.AddDays(15));
			position.Id = cmd.PositionId;
			return position;
		}
	}
}
