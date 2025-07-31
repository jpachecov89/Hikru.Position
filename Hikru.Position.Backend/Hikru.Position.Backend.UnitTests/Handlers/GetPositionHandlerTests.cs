using Hikru.Position.Backend.Application.Exceptions;
using Hikru.Position.Backend.Application.Interfaces.Persistence;
using Hikru.Position.Backend.Application.Positions.Queries.GetPosition;
using Hikru.Position.Backend.Domain.Entities;
using Hikru.Position.Backend.Domain.Enums;
using Moq;

namespace Hikru.Position.Backend.UnitTests.Handlers
{
	public class GetPositionHandlerTests
	{
		private readonly Mock<IUnitOfWork> _uowMock = new();
		private readonly GetPositionHandler _handler;

		public GetPositionHandlerTests()
		{
			_handler = new GetPositionHandler(_uowMock.Object);
		}

		[Fact]
		public async Task Handle_ValidId_ReturnsPositionWithDetails()
		{
			var command = new GetPositionQuery { PositionId = Guid.NewGuid() };

			var position = new Domain.Entities.Position("Dev", "API Dev", "Remote", PositionStatus.Open, Guid.NewGuid(), Guid.NewGuid(), 5000, DateTime.UtcNow)
			{
				Id = command.PositionId,
				Recruiter = new Recruiter { Name = "Ana", Seniority = "Senior" },
				Department = new Department { Name = "TI" }
			};

			_uowMock.Setup(x => x.Positions.GetByIdAsync(command.PositionId))
				.ReturnsAsync(position);

			var result = await _handler.Handle(command, CancellationToken.None);

			Assert.NotNull(result);
			Assert.Equal(command.PositionId, result.PositionId);
			Assert.Equal("Ana (Senior)", result.Recruiter);
			Assert.Equal("TI", result.Department);
		}

		[Fact]
		public async Task Handle_InvalidId_ThrowsNotFoundException()
		{
			var command = new GetPositionQuery { PositionId = Guid.NewGuid() };

			_uowMock.Setup(x => x.Positions.GetByIdAsync(command.PositionId))
				.ReturnsAsync((Domain.Entities.Position)null!);

			await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
		}
	}
}
