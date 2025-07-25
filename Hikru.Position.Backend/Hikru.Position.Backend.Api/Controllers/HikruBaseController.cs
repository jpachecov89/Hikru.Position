using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hikru.Position.Backend.Api.Controllers
{
	[ApiController]
	public class HikruBaseController : ControllerBase
	{
		private IMediator _mediator;

		protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
	}
}
