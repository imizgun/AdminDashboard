using AdminDashboardServer.DatabaseAccess;
using AdminDashboardServer.DTO;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboardServer.Controllers;

public static class PaymentController {
	public static void MapPaymentEndpoints(this WebApplication app) {
		app.MapGet("/payments", async (DashboardDbContext db, int? take, int? skip) => {
				var notNullTake = take ?? 5;
				var notNullSkip = skip ?? 0;
				var payments = await db.Payments
					.Include(p => p.Sender)
					.Include(p => p.Receiver)
					.OrderBy(p => p.PaymentDate)
					.Skip(notNullSkip)
					.Take(notNullTake)
					.Select(
						x => new PaymentDto {
							PaymentId = x.PaymentId,
							SenderId = x.SenderId,
							ReceiverId = x.ReceiverId,
							PaymentDate = x.PaymentDate,
							Amount = x.Amount,
							SenderEmail = x.Sender.Email,
							ReceiverEmail = x.Receiver.Email,
						})
					.ToListAsync();
		
				return Results.Ok(payments);
			})
			.RequireAuthorization()
			.WithOpenApi();
	}
}