using Hikru.Position.Backend.Application.Exceptions;
using Hikru.Position.Backend.Application.Interfaces.Persistence;
using Hikru.Position.Backend.Application.Positions.Commands.CreatePosition;
using Hikru.Position.Backend.Domain.Entities;
using Hikru.Position.Backend.Domain.Enums;
using Moq;

namespace Hikru.Position.Backend.UnitTests.Handlers
{
	public class CreatePositionHandlerTests
	{
		private readonly Mock<IUnitOfWork> _uowMock = new();

		private readonly CreatePositionHandler _handler;

		public CreatePositionHandlerTests()
		{
			_handler = new CreatePositionHandler(_uowMock.Object);
		}

		[Fact]
		public async Task Handle_ValidRequest_ReturnsExpectedResult()
		{
			var command = GetValidCommand();
			var position = GetPositionFromCommand(command);

			_uowMock.Setup(x => x.Positions.AddAsync(It.IsAny<Domain.Entities.Position>()))
				.Returns(Task.CompletedTask);

			_uowMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.ReturnsAsync(1);

			_uowMock.Setup(x => x.Positions.GetByIdAsync(It.IsAny<Guid>()))
				.ReturnsAsync(() =>
				{
					position.Recruiter = GetRecruiterFromCommand(command.RecruiterId);
					position.Department = GetDepartmentFromCommand(command.DepartmentId);
					return position;
				});

			var result = await _handler.Handle(command, CancellationToken.None);

			Assert.NotNull(result);
			Assert.Equal(command.Title, result.Title);
			Assert.Equal("Alice (Senior)", result.Recruiter);
			Assert.Equal("Engineering", result.Department);
		}

		[Fact]
		public async Task Handle_ValidRequest_CallsAddAsyncAndSaveChanges()
		{
			var command = GetValidCommand();
			var position = GetPositionFromCommand(command);
			position.Recruiter = GetRecruiterFromCommand(command.RecruiterId);
			position.Department = GetDepartmentFromCommand(command.DepartmentId);

			_uowMock.Setup(x => x.Positions.AddAsync(It.IsAny<Domain.Entities.Position>()))
				.Returns(Task.CompletedTask);
			_uowMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.ReturnsAsync(1);
			_uowMock.Setup(x => x.Positions.GetByIdAsync(It.IsAny<Guid>()))
				.ReturnsAsync(position);

			await _handler.Handle(command, CancellationToken.None);

			_uowMock.Verify(x => x.Positions.AddAsync(It.IsAny<Domain.Entities.Position>()), Times.Once);
			_uowMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
		}

		[Fact]
		public async Task Handle_ValidRequest_ReturnsRecruiterAndDepartmentInfo()
		{
			var command = GetValidCommand();
			var position = GetPositionFromCommand(command);
			position.Recruiter = GetRecruiterFromCommand(command.RecruiterId);
			position.Department = GetDepartmentFromCommand(command.DepartmentId);

			_uowMock.Setup(x => x.Positions.AddAsync(It.IsAny<Domain.Entities.Position>()))
				.Returns(Task.CompletedTask);
			_uowMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.ReturnsAsync(1);
			_uowMock.Setup(x => x.Positions.GetByIdAsync(It.IsAny<Guid>()))
				.ReturnsAsync(position);

			var result = await _handler.Handle(command, CancellationToken.None);

			Assert.Equal("Alice (Senior)", result.Recruiter);
			Assert.Equal("Engineering", result.Department);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public async Task Handle_TitleIsInvalid_ThrowsBadRequestException(string? title)
		{
			var command = GetValidCommand();
			command.Title = title;

			await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public async Task Handle_DescriptionIsEmpty_ThrowsBadRequestException(string description)
		{
			var command = GetValidCommand();
			command.Description = description;

			await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public async Task Handle_LocationIsEmpty_ThrowsBadRequestException(string location)
		{
			var command = GetValidCommand();
			command.Location = location;

			await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
		}

		[Fact]
		public async Task Handle_BudgetIsZero_ThrowsBadRequestException()
		{
			var command = GetValidCommand();
			command.Budget = 0;

			await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
		}

		[Fact]
		public async Task Handle_GetByIdReturnsNull_ThrowsBadRequestException()
		{
			var command = GetValidCommand();

			_uowMock.Setup(x => x.Positions.AddAsync(It.IsAny<Domain.Entities.Position>()))
				.Returns(Task.CompletedTask);
			_uowMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.ReturnsAsync(1);
			_uowMock.Setup(x => x.Positions.GetByIdAsync(It.IsAny<Guid>()))
				.ReturnsAsync((Domain.Entities.Position)null!);

			await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
		}

		private CreatePositionCommand GetValidCommand() => new()
		{
			Title = "Dev",
			Description = "Develop backend",
			Location = "Lima",
			Status = PositionStatus.Open,
			RecruiterId = Guid.NewGuid(),
			DepartmentId = Guid.NewGuid(),
			Budget = 5000,
			ClosingDate = DateTime.UtcNow.AddDays(10)
		};

		private Domain.Entities.Position GetPositionFromCommand(CreatePositionCommand cmd) => new(
			cmd.Title,
			cmd.Description,
			cmd.Location,
			cmd.Status,
			cmd.RecruiterId,
			cmd.DepartmentId,
			cmd.Budget,
			cmd.ClosingDate);

		private Recruiter GetRecruiterFromCommand(Guid recruiterId) => new Recruiter
		{
			Id = recruiterId,
			Name = "Alice",
			Email = "alice.graham@hikru.com",
			Phone = "+51 987 156 135",
			Seniority = "Senior"
		};

		private Department GetDepartmentFromCommand(Guid departmentId) => new Department
		{
			Id = departmentId,
			Name = "Engineering",
			Description = "Engineering for Tests"
		};
	}
}
