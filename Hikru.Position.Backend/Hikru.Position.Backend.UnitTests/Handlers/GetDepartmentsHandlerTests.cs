using Hikru.Position.Backend.Application.Departments.Queries.GetDepartments;
using Hikru.Position.Backend.Application.Interfaces.Persistence;
using Hikru.Position.Backend.Domain.Entities;
using Moq;

namespace Hikru.Position.Backend.UnitTests.Handlers
{
	public class GetDepartmentsHandlerTests
	{
		private readonly Mock<IUnitOfWork> _uowMock = new();
		private readonly GetDepartmentsHandler _handler;

		public GetDepartmentsHandlerTests()
		{
			_handler = new GetDepartmentsHandler(_uowMock.Object);
		}

		[Fact]
		public async Task Handle_WhenCalled_ReturnsAllDepartments()
		{
			var departments = new List<Department>
		{
			new Department { Id = Guid.NewGuid(), Name = "IT", Description = "Tech department" },
			new Department { Id = Guid.NewGuid(), Name = "HR", Description = "People department" }
		};

			_uowMock.Setup(x => x.Departments.GetAllAsync())
				.ReturnsAsync(departments);

			var result = await _handler.Handle(new GetDepartmentsQuery(), CancellationToken.None);

			Assert.Equal(2, result.Count);
			Assert.Contains(result, d => d.Name == "IT");
			Assert.Contains(result, d => d.Name == "HR");
		}

		[Fact]
		public async Task Handle_WhenNoDepartments_ReturnsEmptyList()
		{
			_uowMock.Setup(x => x.Departments.GetAllAsync())
				.ReturnsAsync(new List<Department>());

			var result = await _handler.Handle(new GetDepartmentsQuery(), CancellationToken.None);

			Assert.NotNull(result);
			Assert.Empty(result);
		}
	}
}
