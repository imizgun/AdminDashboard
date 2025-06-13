using AdminDashboardServer.DatabaseAccess.Configurations;
using AdminDashboardServer.DatabaseAccess.Domain;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboardServer.DatabaseAccess;

public class DashboardDbContext : DbContext {
	public DbSet<Client> Clients { get; set; }
	public DbSet<Payment> Payments { get; set; }
	public DbSet<Admin> Admins { get; set; }
	
	public DashboardDbContext(DbContextOptions<DashboardDbContext> options) : base(options) {}

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.ApplyConfiguration(new ClientConfiguration());
		modelBuilder.ApplyConfiguration(new PaymentConfiguration());
		modelBuilder.ApplyConfiguration(new AdminConfiguration());
	}
}