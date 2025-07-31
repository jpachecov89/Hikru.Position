using MediatR;

namespace Hikru.Position.Backend.Application.Auth.Commands.CreateAuth
{
	public class CreateAuthCommand : IRequest<CreateAuthResult>
	{
		public string Username { get; set; } = null!;
		public string Password { get; set; } = null!;
	}
}
