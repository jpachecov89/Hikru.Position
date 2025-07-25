namespace Hikru.Position.Backend.Domain.Entities
{
	public class Recruiter
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string Phone { get; set; } = null!;
		public string Seniority { get; set; } = null!;
	}
}
