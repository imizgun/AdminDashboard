using AdminDashboardServer.DatabaseAccess;
using AdminDashboardServer.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboardServer.Controllers;

public static class AuthController {
	public static void MapAuthEndpoints(this WebApplication app) {
		
		app.MapPost("auth/login", async (DashboardDbContext context, Hasher hasher, TokenProvider tokenProvider, [FromBody] LoginDto dto) => {
			var dbAdmin = await context.Admins.FirstOrDefaultAsync(a => a.Email == dto.Email);

			if (dbAdmin is null) return Results.NotFound(new { message = "Email address not found" });

			return hasher.PasswordHasher.VerifyHashedPassword(null, dbAdmin.Password, dto.Password) == PasswordVerificationResult.Success 
				? Results.Ok(new { token = tokenProvider.GenerateAccessToken(dbAdmin) }) 
				: Results.Unauthorized();
		}).WithOpenApi();
	}
}