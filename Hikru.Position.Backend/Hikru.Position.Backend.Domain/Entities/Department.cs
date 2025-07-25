namespace Hikru.Position.Backend.Domain.Entities
{
	public class Department
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;

	}
}
