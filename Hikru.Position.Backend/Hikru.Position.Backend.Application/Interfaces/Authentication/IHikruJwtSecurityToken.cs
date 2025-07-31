namespace Hikru.Position.Backend.Application.Interfaces.Authentication
{
	public interface IHikruJwtSecurityToken
	{
		string GetAuthenticationToken(string username);
		void ValidateToken(string tokenValue);
	}
}
