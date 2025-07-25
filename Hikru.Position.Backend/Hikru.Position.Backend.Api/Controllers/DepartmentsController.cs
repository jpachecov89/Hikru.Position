using Hikru.Position.Backend.Application.Departments.Queries.GetDepartments;
using Microsoft.AspNetCore.Mvc;

namespace Hikru.Position.Backend.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class DepartmentsController : HikruBaseController
	{
		[HttpGet]
		public async Task<IActionResult> GetDepartments()
		{
			var result = await Mediator.Send(new GetDepartmentsQuery());
			return Ok(result);
		}
	}
}
