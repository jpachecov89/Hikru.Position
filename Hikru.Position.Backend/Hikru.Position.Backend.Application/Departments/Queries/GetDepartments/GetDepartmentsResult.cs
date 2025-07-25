namespace Hikru.Position.Backend.Application.Departments.Queries.GetDepartments
{
	public class GetDepartmentsResult
	{
		public Guid DepartmentId { get; set; }
		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;
	}
}
