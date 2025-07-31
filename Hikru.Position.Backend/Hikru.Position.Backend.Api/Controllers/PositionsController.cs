using Hikru.Position.Backend.Application.Exceptions;
using Hikru.Position.Backend.Application.Positions.Commands.CreatePosition;
using Hikru.Position.Backend.Application.Positions.Commands.DeletePosition;
using Hikru.Position.Backend.Application.Positions.Commands.UpdatePosition;
using Hikru.Position.Backend.Application.Positions.Queries.GetPosition;
using Hikru.Position.Backend.Application.Positions.Queries.GetPositions;
using Microsoft.AspNetCore.Mvc;

namespace Hikru.Position.Backend.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PositionsController : HikruBaseController
	{
		[HttpGet]
		public async Task<IActionResult> GetPositions()
		{
			var result = await Mediator.Send(new GetPositionsQuery());
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetPosition([FromRoute] string id)
		{
			if (!Guid.TryParse(id, out var positionId))
				throw new BadRequestException($"Id: '{id}' is not a correct Guid");

			var result = await Mediator.Send(new GetPositionQuery { PositionId = positionId });
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> CreatePosition([FromBody] CreatePositionCommand request)
		{
			var result = await Mediator.Send(request);
			return Created("api/positions/" + result.PositionId, result);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdatePosition([FromBody] UpdatePositionCommand request, [FromRoute] string id)
		{
			if (!Guid.TryParse(id, out var positionId))
				throw new BadRequestException($"Id: '{id}' is not a correct Guid");

			request.PositionId = positionId;
			var result = await Mediator.Send(request);
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePosition([FromRoute] string id)
		{
			if (!Guid.TryParse(id, out var positionId))
				throw new BadRequestException($"Id: '{id}' is not a correct Guid");

			var result = await Mediator.Send(new DeletePositionCommand { PositionId = positionId });
			return NoContent();
		}
	}
}
