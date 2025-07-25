namespace Hikru.Position.Backend.Application.Recruiters.Queries.GetRecruiters
{
	public class GetRecruitersResult
	{
		public Guid RecruiterId { get; set; }
		public string Name { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string Phone { get; set; } = null!;
		public string Seniority { get; set; } = null!;
	}
}
