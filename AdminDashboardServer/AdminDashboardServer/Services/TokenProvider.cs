using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AdminDashboardServer.DatabaseAccess.Domain;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace AdminDashboardServer;

public class TokenProvider {
	public string GenerateAccessToken(Admin admin)
	{
		var claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Sub, admin.AdminId.ToString()),
			new Claim(JwtRegisteredClaimNames.Email, admin.Email)
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.SecretKey));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var tokenDescriptor = new SecurityTokenDescriptor {
			Subject = new ClaimsIdentity(claims),
			Expires = DateTime.UtcNow.AddMinutes(JwtSettings.AccessTokenLifeTimeMinutes),
			SigningCredentials = creds,
			Issuer = JwtSettings.Issuer,
			Audience = JwtSettings.Audience
		};

		var handler = new JsonWebTokenHandler();
		return handler.CreateToken(tokenDescriptor);
	}

}