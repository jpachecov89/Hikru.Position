using Hikru.Position.Backend.Application.Interfaces.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hikru.Position.Backend.Infrastructure.Authentication
{
	public class HikruJwtSecurityToken : IHikruJwtSecurityToken
	{
		private readonly IConfiguration _config;
		public HikruJwtSecurityToken(IConfiguration config)
		{
			_config = config;
		}

		public string GetAuthenticationToken(string username)
		{
			int expires = int.Parse(_config.GetSection("Auth")["Expires"]!);
			string symmetricSecurityKey = _config.GetSection("Auth")["SymmetricSecurityKey"]!;

			JwtSecurityToken jwt = new JwtSecurityToken(claims: new List<Claim> { new Claim(ClaimTypes.Name, username) },
				expires: DateTime.UtcNow.AddMinutes(expires),
				signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(symmetricSecurityKey)), SecurityAlgorithms.HmacSha256));

			return new JwtSecurityTokenHandler().WriteToken(jwt);
		}

		public void ValidateToken(string tokenValue)
		{
			string symmetricSecurityKey = _config.GetSection("Auth")["SymmetricSecurityKey"]!;

			var tokenHandler = new JwtSecurityTokenHandler();
			tokenHandler.ValidateToken(tokenValue, new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(symmetricSecurityKey)),
				ValidateIssuer = false,
				ValidateAudience = false,
				ClockSkew = TimeSpan.Zero
			}, out SecurityToken validatedToken);
		}
	}
}
