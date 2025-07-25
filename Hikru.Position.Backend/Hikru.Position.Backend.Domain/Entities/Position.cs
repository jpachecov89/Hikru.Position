using Hikru.Position.Backend.Domain.Enums;

namespace Hikru.Position.Backend.Domain.Entities
{
	public class Position
	{
		public Guid Id { get; set; }
		public string Title { get; set; } = null!;
		public string Description { get; set; } = null!;
		public string Location { get; set; } = null!;
		public PositionStatus Status { get; set; }
		public Guid RecruiterId { get; set; }
		public Guid DepartmentId { get; set; }
		public double Budget { get; set; }
		public DateTime? ClosingDate { get; set; }

		public Recruiter Recruiter { get; set; } = null!;
		public Department Department { get; set; } = null!;
	}
}
