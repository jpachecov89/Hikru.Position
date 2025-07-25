using Hikru.Position.Backend.Application.Recruiters.Queries.GetRecruiters;
using Microsoft.AspNetCore.Mvc;

namespace Hikru.Position.Backend.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class RecruitersController : HikruBaseController
	{
		[HttpGet]
		public async Task<IActionResult> GetRecruiters()
		{
			var result = await Mediator.Send(new GetRecruitersQuery());
			return Ok(result);
		}
	}
}
