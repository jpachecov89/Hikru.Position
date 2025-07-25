using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hikru.Position.Backend.Application.Recruiters.Queries.GetRecruiters
{
	public class GetRecruitersQuery : IRequest<List<GetRecruitersResult>>
	{
	}
}
