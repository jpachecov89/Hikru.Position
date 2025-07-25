using Hikru.Position.Backend.Application.Interfaces.Persistence;
using MediatR;

namespace Hikru.Position.Backend.Application.Recruiters.Queries.GetRecruiters
{
	public class GetRecruitersHandler : IRequestHandler<GetRecruitersQuery, List<GetRecruitersResult>>
	{
		private readonly IUnitOfWork _uow;

		public GetRecruitersHandler(IUnitOfWork uow)
		{
			_uow = uow;
		}

		public async Task<List<GetRecruitersResult>> Handle(GetRecruitersQuery request, CancellationToken cancellationToken)
		{
			var recruiters = await _uow.Recruiters.GetAllAsync();

			return recruiters.Select(x => new GetRecruitersResult
			{
				RecruiterId = x.Id,
				Name = x.Name,
				Email = x.Email,
				Phone = x.Phone,
				Seniority = x.Seniority
			}).ToList();
		}
	}
}
