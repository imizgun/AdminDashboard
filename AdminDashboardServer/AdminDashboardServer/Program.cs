using AdminDashboardServer;
using AdminDashboardServer.Controllers;
using AdminDashboardServer.DatabaseAccess;
using AdminDashboardServer.DatabaseAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Payment = AdminDashboardServer.DatabaseAccess.Domain.Payment;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<GlobalRateState>();
builder.Services.AddSingleton<Hasher>();
builder.Services.AddDbContext<DashboardDbContext>(opt => {
	opt.UseNpgsql("Username=postgres;Password=123;Host=localhost;Port=5432;Database=dashboard;");
});

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

// Дефолтные значения при запуске
using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<DashboardDbContext>();
	
	if (!db.Clients.Any() && !db.Payments.Any()) {
		var hasher = scope.ServiceProvider.GetRequiredService<Hasher>();
		db.Clients.RemoveRange(db.Clients);
		db.Payments.RemoveRange(db.Payments);
		
		List<Client> clients = [
			Client.CreateNew(
				"Bob", 
				"bob@gmail.com", 
				1000, 
				[], 
				[]
				),
			Client.CreateNew(
				"Alex", 
				"alex@gmail.com", 
				2000, 
				[], 
				[]
			),
			Client.CreateNew(
				"John", 
				"john@gmail.com", 
				1500, 
				[], 
				[]
			)
		];

		List<Payment> payments = [
			Payment.CreateNew(clients[0].ClientId, clients[1].ClientId, 500, DateTime.UtcNow),
			Payment.CreateNew(clients[1].ClientId, clients[2].ClientId, 200, DateTime.UtcNow),
			Payment.CreateNew(clients[2].ClientId, clients[0].ClientId, 300, DateTime.UtcNow),
			Payment.CreateNew(clients[1].ClientId, clients[0].ClientId, 700, DateTime.UtcNow),
			Payment.CreateNew(clients[2].ClientId, clients[1].ClientId, 100, DateTime.UtcNow),
		];

		payments[0].Sender = clients[0];
		payments[1].Sender = clients[1];
		payments[2].Sender = clients[2];
		payments[3].Sender = clients[1];
		payments[4].Sender = clients[2];
		
		payments[0].Receiver = clients[1];
		payments[1].Receiver = clients[2];
		payments[2].Receiver = clients[0];
		payments[3].Receiver = clients[0];
		payments[4].Receiver = clients[1];

		var admin = Admin.CreateAdmin("admin@mirra.dev", hasher.PasswordHasher.HashPassword(null, "admin123"));
		
		await db.Clients.AddRangeAsync(clients);
		await db.Payments.AddRangeAsync(payments);
		await db.Admins.AddAsync(admin);
		await db.SaveChangesAsync();
	}
	
	db.Database.Migrate();
}

app.UseHttpsRedirection();
app.MapClientEndpoints();
app.MapPaymentEndpoints();
app.MapRateEndpoints();
app.MapAuthEndpoints();

app.Run();