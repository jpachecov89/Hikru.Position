using Hikru.Position.Backend.Application.Positions.Commands.CreatePosition;
using Hikru.Position.Backend.Application.Positions.Commands.DeletePosition;
using Hikru.Position.Backend.Application.Positions.Commands.UpdatePosition;
using Hikru.Position.Backend.Application.Positions.Queries.GetPosition;
using Hikru.Position.Backend.Application.Positions.Queries.GetPositions;
using Microsoft.AspNetCore.Mvc;

namespace Hikru.Position.Backend.Api.Controllers
{
	[ApiController]
	public class PositionController : HikruBaseController
	{
		[HttpGet("api/positions")]
		public async Task<IActionResult> GetPositions()
		{
			var result = await Mediator.Send(new GetPositionsQuery());
			return Ok(result);
		}

		[HttpGet("api/positions/{id}")]
		public async Task<IActionResult> GetPosition([FromRoute] string id)
		{
			if (!Guid.TryParse(id, out var positionId))
			{
				return BadRequest();
			}

			var result = await Mediator.Send(new GetPositionQuery { PositionId = positionId });
			return Ok(result);
		}

		[HttpPost("api/positions")]
		public async Task<IActionResult> CreatePosition([FromBody] CreatePositionCommand request)
		{
			var result = await Mediator.Send(request);
			return Created("api/positions/" + result.PositionId, result);
		}

		[HttpPut("api/positions/{id}")]
		public async Task<IActionResult> UpdatePosition([FromBody] UpdatePositionCommand request, [FromRoute] string id)
		{
			if (!Guid.TryParse(id, out var positionId))
			{
				return BadRequest();
			}

			request.PositionId = positionId;
			var result = await Mediator.Send(request);
			return Ok(result);
		}

		[HttpDelete("api/positions/{id}")]
		public async Task<IActionResult> DeletePosition([FromRoute] string id)
		{
			if (!Guid.TryParse(id, out var positionId))
			{
				return BadRequest();
			}

			var result = await Mediator.Send(new DeletePositionCommand { PositionId = positionId });
			return NoContent();
		}
	}
}
