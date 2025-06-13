using AdminDashboardServer.DatabaseAccess;
using AdminDashboardServer.DatabaseAccess.Domain;
using AdminDashboardServer.DTO;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboardServer.Controllers;

public static class ClientsController {
	public static void MapClientEndpoints(this WebApplication app) {
		
		app.MapGet("/clients", async (DashboardDbContext db) => {
				var clients = await db.Clients.Select(
						x => new ClientDto {
							Name = x.Name, 
							ClientId = x.ClientId, 
							BalanceT = x.BalanceT, 
							Email = x.Email
						})
					.ToListAsync();
		
				return Results.Ok(clients);
			})
			.WithOpenApi();

		app.MapPost("/clients", async (DashboardDbContext db, PostClient client) => {
			var sameEmailClient = await db.Clients.FirstOrDefaultAsync(x => x.Email == client.Email);
			
			if (sameEmailClient is not null) return Results.BadRequest(new {message = "Client already exists"});
			
			var newClient = Client.CreateNew(client.Name, client.Email, client.BalanceT, [], []);
			await db.Clients.AddAsync(newClient);
			await db.SaveChangesAsync();
			
			return Results.Ok(new {clientId = newClient.ClientId});
		});
		
		app.MapPut("/clients/{id}", async (DashboardDbContext db, UpdateClientDto client, Guid id) => {
			var sameEmailClient = await db.Clients.FirstOrDefaultAsync(x => x.Email == client.Email);
			
			if (sameEmailClient is not null) return Results.BadRequest(new {message = "Email already exists"});
			
			if (!await db.Clients.AnyAsync(x => x.ClientId == id)) return Results.NotFound(new {message = "Client not found"});
			
			var updateRes = await db.Clients.Where(x => x.ClientId == id).ExecuteUpdateAsync(
				c => c
					.SetProperty(x => x.Email, client.Email)
					.SetProperty(x => x.BalanceT, client.BalanceT)
					.SetProperty(x => x.Name, client.Name)
				);
			await db.SaveChangesAsync();
			
			return updateRes > 0 ? Results.Ok(new {message = "Updated successfully"}) :  Results.NotFound();
		});

		app.MapDelete("/clients/{id}", async (DashboardDbContext db, Guid id) => {
			var deleteRes = await db.Clients.Where(x => x.ClientId == id).ExecuteDeleteAsync();

			return deleteRes > 0 ? Results.Ok() : Results.NotFound();
		});
	}
}