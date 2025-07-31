using Hikru.Position.Backend.Application.Exceptions;
using Hikru.Position.Backend.Application.Interfaces.Authentication;
using MediatR;

namespace Hikru.Position.Backend.Application.Auth.Commands.CreateAuth
{
	public class CreateAuthHandler : IRequestHandler<CreateAuthCommand, CreateAuthResult>
	{
		private readonly IHikruJwtSecurityToken _tokenHandler;
		public CreateAuthHandler(IHikruJwtSecurityToken tokenHandler)
		{
			_tokenHandler = tokenHandler;
		}

		public Task<CreateAuthResult> Handle(CreateAuthCommand request, CancellationToken cancellationToken)
		{
			if (string.Equals(request.Username, "Admin", StringComparison.OrdinalIgnoreCase) && string.Equals(request.Password, "admin", StringComparison.OrdinalIgnoreCase))
			{
				var result = new CreateAuthResult
				{
					Token = _tokenHandler.GetAuthenticationToken(request.Username)
				};

				return Task.FromResult(result);
			}
			else
			{
				throw new BadRequestException("Invalid credentials");
			}
		}
	}
}
