using AdminDashboardServer.DatabaseAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminDashboardServer.DatabaseAccess.Configurations;

public class ClientConfiguration: IEntityTypeConfiguration<Client> {
	
	public void Configure(EntityTypeBuilder<Client> builder) {
		builder.HasKey(c => c.ClientId);
		builder.Property(c => c.BalanceT).IsRequired();
		builder.Property(c => c.Email).IsRequired();
		builder.Property(c => c.Name).IsRequired();

		builder.HasMany(c => c.ClientPayments)
			.WithOne(p => p.Sender)
			.HasForeignKey(p => p.SenderId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasMany(c => c.ClientIncomings)
			.WithOne(p => p.Receiver)
			.HasForeignKey(p => p.ReceiverId)
			.OnDelete(DeleteBehavior.NoAction);
			
	}
	
}