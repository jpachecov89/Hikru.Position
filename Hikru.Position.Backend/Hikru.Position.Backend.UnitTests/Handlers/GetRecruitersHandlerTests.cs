using Hikru.Position.Backend.Application.Interfaces.Persistence;
using Hikru.Position.Backend.Application.Recruiters.Queries.GetRecruiters;
using Hikru.Position.Backend.Domain.Entities;
using Moq;

namespace Hikru.Position.Backend.UnitTests.Handlers
{
	public class GetRecruitersHandlerTests
	{
		private readonly Mock<IUnitOfWork> _uowMock = new();
		private readonly GetRecruitersHandler _handler;

		public GetRecruitersHandlerTests()
		{
			_handler = new GetRecruitersHandler(_uowMock.Object);
		}

		[Fact]
		public async Task Handle_WhenCalled_ReturnsAllRecruiters()
		{
			var recruiters = new List<Recruiter>
		{
			new Recruiter { Id = Guid.NewGuid(), Name = "Alice", Email = "alice@test.com", Phone = "123456", Seniority = "Senior" },
			new Recruiter { Id = Guid.NewGuid(), Name = "Bob", Email = "bob@test.com", Phone = "654321", Seniority = "Mid" }
		};

			_uowMock.Setup(x => x.Recruiters.GetAllAsync())
				.ReturnsAsync(recruiters);

			var result = await _handler.Handle(new GetRecruitersQuery(), CancellationToken.None);

			Assert.Equal(2, result.Count);
			Assert.Contains(result, r => r.Name == "Alice" && r.Seniority == "Senior");
			Assert.Contains(result, r => r.Name == "Bob" && r.Seniority == "Mid");
		}

		[Fact]
		public async Task Handle_WhenNoRecruiters_ReturnsEmptyList()
		{
			_uowMock.Setup(x => x.Recruiters.GetAllAsync())
				.ReturnsAsync(new List<Recruiter>());

			var result = await _handler.Handle(new GetRecruitersQuery(), CancellationToken.None);

			Assert.NotNull(result);
			Assert.Empty(result);
		}
	}
}
