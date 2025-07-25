using Hikru.Position.Backend.Application.Interfaces.Persistence;
using MediatR;

namespace Hikru.Position.Backend.Application.Departments.Queries.GetDepartments
{
	public class GetDepartmentsHandler : IRequestHandler<GetDepartmentsQuery, List<GetDepartmentsResult>>
	{
		private readonly IUnitOfWork _uow;

		public GetDepartmentsHandler(IUnitOfWork uow)
		{
			_uow = uow;
		}

		public async Task<List<GetDepartmentsResult>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
		{
			var departments = await _uow.Departments.GetAllAsync();

			return departments.Select(x => new GetDepartmentsResult
			{
				DepartmentId = x.Id,
				Name = x.Name,
				Description = x.Description
			}).ToList();
		}
	}
}
