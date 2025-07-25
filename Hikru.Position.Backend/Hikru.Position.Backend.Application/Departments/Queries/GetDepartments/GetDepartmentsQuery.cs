using MediatR;

namespace Hikru.Position.Backend.Application.Departments.Queries.GetDepartments
{
	public class GetDepartmentsQuery : IRequest<List<GetDepartmentsResult>>
	{
	}
}
