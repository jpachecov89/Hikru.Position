using Hikru.Position.Backend.Application.Auth.Commands.CreateAuth;
using Microsoft.AspNetCore.Mvc;

namespace Hikru.Position.Backend.Api.Controllers
{
	[Route("api/[controller]")]
	public class AuthController : HikruBaseController
	{
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromForm] CreateAuthCommand request)
		{
			var result = await Mediator.Send(request);
			return Ok(result);
		}
	}
}
