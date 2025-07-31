using Hikru.Position.Backend.Application.Interfaces.Persistence;
using Hikru.Position.Backend.Application.Positions.Queries.GetPositions;
using Hikru.Position.Backend.Domain.Entities;
using Hikru.Position.Backend.Domain.Enums;
using Moq;

namespace Hikru.Position.Backend.UnitTests.Handlers
{
	public class GetPositionsHandlerTests
	{
		private readonly Mock<IUnitOfWork> _uowMock = new();
		private readonly GetPositionsHandler _handler;

		public GetPositionsHandlerTests()
		{
			_handler = new GetPositionsHandler(_uowMock.Object);
		}

		[Fact]
		public async Task Handle_WhenCalled_ReturnsAllPositionsWithDetails()
		{
			var recruiter = new Recruiter { Id = Guid.NewGuid(), Name = "Alice", Seniority = "Senior" };
			var department = new Department { Id = Guid.NewGuid(), Name = "Engineering" };

			var positions = new List<Domain.Entities.Position>
			{
				new Domain.Entities.Position("Dev", "Build things", "Remote", PositionStatus.Open, recruiter.Id, department.Id, 1000, DateTime.UtcNow)
				{
					Recruiter = recruiter,
					Department = department
				},
				new Domain.Entities.Position("Tester", "Test things", "Onsite", PositionStatus.Closed, recruiter.Id, department.Id, 800, DateTime.UtcNow)
				{
					Recruiter = recruiter,
					Department = department
				}
			};

			_uowMock.Setup(x => x.Positions.GetAllAsync())
				.ReturnsAsync(positions);

			var result = await _handler.Handle(new GetPositionsQuery(), CancellationToken.None);

			Assert.Equal(2, result.Count);
			Assert.All(result, item => Assert.False(string.IsNullOrWhiteSpace(item.Recruiter)));
			Assert.All(result, item => Assert.False(string.IsNullOrWhiteSpace(item.Department)));
			Assert.Equal("Alice (Senior)", result[0].Recruiter);
			Assert.Equal("Engineering", result[0].Department);
		}

		[Fact]
		public async Task Handle_WhenNoPositions_ReturnsEmptyList()
		{
			_uowMock.Setup(x => x.Positions.GetAllAsync())
				.ReturnsAsync(new List<Domain.Entities.Position>());

			var result = await _handler.Handle(new GetPositionsQuery(), CancellationToken.None);

			Assert.NotNull(result);
			Assert.Empty(result);
		}
	}
}
