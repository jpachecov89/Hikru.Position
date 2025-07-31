using Hikru.Position.Backend.Application.Exceptions;
using Hikru.Position.Backend.Application.Interfaces.Authentication;

namespace Hikru.Position.Backend.Api.Middlewares
{
	public class JwtAuthorizationMiddleware
	{
		private readonly RequestDelegate _next;
		public JwtAuthorizationMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context, IHikruJwtSecurityToken tokenHandler)
		{
			var excludedPaths = new[]
			{
				"/api/auth/login",
				"/api/auth/logout"
			};

			if (excludedPaths.Contains(context.Request.Path.Value?.ToLowerInvariant()))
			{
				await _next(context);
				return;
			}

			var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

			if (string.IsNullOrEmpty(token))
				throw new BadRequestException("Api Key or Token Value Not Found");

			try
			{
				tokenHandler.ValidateToken(token);
			}
			catch
			{
				throw new ForbiddenException("Invalid Token value");
			}

			await _next(context);
		}
	}
}
