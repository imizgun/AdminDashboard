using AdminDashboardServer.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboardServer.Controllers;

public static class RateController {
	public static void MapRateEndpoints(this WebApplication app) {
		app.MapGet("/rate", (GlobalRateState gs) => Results.Ok(new RateDto {Rate = gs.Rate}))
			.RequireAuthorization();

		app.MapPost("/rate", (GlobalRateState gs, [FromBody] RateDto dto) => {
			if (dto.Rate <= 0) 
				return Results.BadRequest( new 
				{
					message = "Rate must be greater than 0"
				});
			
			gs.Rate = dto.Rate;
			return Results.Ok(new 
			{
				message = $"Rate successfully updated to {gs.Rate}"
			});
		})
		.RequireAuthorization();
	}
}