using Hikru.Position.Backend.Application.Exceptions;
using Hikru.Position.Backend.Application.Interfaces.Persistence;
using Hikru.Position.Backend.Application.Positions.Commands.UpdatePosition;
using Hikru.Position.Backend.Domain.Entities;
using Hikru.Position.Backend.Domain.Enums;
using Moq;

namespace Hikru.Position.Backend.UnitTests.Handlers
{
	public class UpdatePositionHandlerTests
	{
		private readonly Mock<IUnitOfWork> _uowMock = new();

		private readonly UpdatePositionHandler _handler;

		public UpdatePositionHandlerTests()
		{
			_handler = new UpdatePositionHandler(_uowMock.Object);
		}

		[Fact]
		public async Task Handle_ValidRequest_UpdatesAndReturnsResult()
		{
			var command = GetValidCommand();
			var position = GetPositionFromCommand(command);
			position.Recruiter = GetRecruiterFromCommand(command.RecruiterId);
			position.Department = GetDepartmentFromCommand(command.DepartmentId);

			_uowMock.Setup(x => x.Positions.GetByIdAsync(command.PositionId))
				.ReturnsAsync(position);
			_uowMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.ReturnsAsync(1);

			var updatedPosition = position;
			_uowMock.Setup(x => x.Positions.GetByIdAsync(position.Id))
				.ReturnsAsync(updatedPosition);

			var result = await _handler.Handle(command, CancellationToken.None);

			Assert.NotNull(result);
			Assert.Equal(command.Title, result.Title);
			Assert.Equal("Alice (Senior)", result.Recruiter);
			Assert.Equal("Engineering", result.Department);
		}

		[Fact]
		public async Task Handle_ValidRequest_CallsUpdateAndSaveChanges()
		{
			var command = GetValidCommand();
			var position = GetPositionFromCommand(command);
			position.Recruiter = GetRecruiterFromCommand(command.RecruiterId);
			position.Department = GetDepartmentFromCommand(command.DepartmentId);

			_uowMock.Setup(x => x.Positions.GetByIdAsync(command.PositionId))
				.ReturnsAsync(position);
			_uowMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.ReturnsAsync(1);
			_uowMock.Setup(x => x.Positions.GetByIdAsync(position.Id))
				.ReturnsAsync(position);

			await _handler.Handle(command, CancellationToken.None);

			_uowMock.Verify(x => x.Positions.Update(It.IsAny<Domain.Entities.Position>()), Times.Once);
			_uowMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
		}

		[Fact]
		public async Task Handle_ValidRequest_ReturnsRecruiterAndDepartmentInfo()
		{
			var command = GetValidCommand();
			var position = GetPositionFromCommand(command);
			position.Recruiter = GetRecruiterFromCommand(command.RecruiterId);
			position.Department = GetDepartmentFromCommand(command.DepartmentId);

			_uowMock.Setup(x => x.Positions.GetByIdAsync(command.PositionId))
				.ReturnsAsync(position);
			_uowMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.ReturnsAsync(1);
			_uowMock.Setup(x => x.Positions.GetByIdAsync(position.Id))
				.ReturnsAsync(position);

			var result = await _handler.Handle(command, CancellationToken.None);

			Assert.Equal("Alice (Senior)", result.Recruiter);
			Assert.Equal("Engineering", result.Department);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public async Task Handle_TitleIsEmpty_ThrowsBadRequestException(string title)
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
		public async Task Handle_PositionNotFound_ThrowsNotFoundException()
		{
			var command = GetValidCommand();
			_uowMock.Setup(x => x.Positions.GetByIdAsync(command.PositionId))
				.ReturnsAsync((Domain.Entities.Position)null!);

			await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
		}

		[Fact]
		public async Task Handle_GetByIdAfterUpdateReturnsNull_ThrowsBadRequestException()
		{
			var command = GetValidCommand();
			var position = GetPositionFromCommand(command);
			position.Recruiter = GetRecruiterFromCommand(command.RecruiterId);
			position.Department = GetDepartmentFromCommand(command.DepartmentId);

			_uowMock.Setup(x => x.Positions.GetByIdAsync(command.PositionId))
				.ReturnsAsync(position);
			_uowMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.ReturnsAsync(1);
			_uowMock.Setup(x => x.Positions.GetByIdAsync(position.Id))
				.ReturnsAsync((Domain.Entities.Position)null!);

			await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
		}

		private UpdatePositionCommand GetValidCommand() => new()
		{
			PositionId = Guid.NewGuid(),
			Title = "Dev",
			Description = "Update backend",
			Location = "Remote",
			Status = PositionStatus.Open,
			RecruiterId = Guid.NewGuid(),
			DepartmentId = Guid.NewGuid(),
			Budget = 10000,
			ClosingDate = DateTime.UtcNow.AddDays(15)
		};

		private Domain.Entities.Position GetPositionFromCommand(UpdatePositionCommand cmd)
		{
			Domain.Entities.Position position = new(
				cmd.Title,
				cmd.Description,
				cmd.Location,
				cmd.Status,
				cmd.RecruiterId,
				cmd.DepartmentId,
				cmd.Budget,
				cmd.ClosingDate);
			position.Id = cmd.PositionId;
			return position;
		}

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
