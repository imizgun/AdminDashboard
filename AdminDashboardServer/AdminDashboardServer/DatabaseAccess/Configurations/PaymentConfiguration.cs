using AdminDashboardServer.DatabaseAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminDashboardServer.DatabaseAccess.Configurations;

public class PaymentConfiguration: IEntityTypeConfiguration<Payment> {
	
	public void Configure(EntityTypeBuilder<Payment> builder) {
		builder.HasKey(p => p.PaymentId);
		builder.Property(p => p.ReceiverId).IsRequired();
		builder.Property(p => p.SenderId).IsRequired();
		builder.Property(p => p.PaymentDate).IsRequired();
		builder.Property(p => p.Amount).IsRequired();

		builder
			.HasOne(p => p.Sender)
			.WithMany(s => s.ClientPayments)
			.HasForeignKey(p => p.SenderId);

		builder
			.HasOne(p => p.Receiver)
			.WithMany(s => s.ClientIncomings)
			.HasForeignKey(p=> p.ReceiverId);
	}
	
}