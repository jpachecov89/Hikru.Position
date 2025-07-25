using Hikru.Position.Backend.Domain.Enums;

namespace Hikru.Position.Backend.Application.Positions.Queries.GetPosition
{
	public class GetPositionResult
	{
		public Guid PositionId { get; set; }
		public string Title { get; set; } = null!;
		public string Description { get; set; } = null!;
		public string Location { get; set; } = null!;
		public PositionStatus Status { get; set; }
		public Guid RecruiterId { get; set; }
		public string Recruiter { get; set; } = null!;
		public Guid DepartmentId { get; set; }
		public string Department { get; set; } = null!;
		public double Budget { get; set; }
		public DateTime? ClosingDate { get; set; }
	}
}
