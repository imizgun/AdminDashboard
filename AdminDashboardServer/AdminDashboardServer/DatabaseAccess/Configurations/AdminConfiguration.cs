using AdminDashboardServer.DatabaseAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminDashboardServer.DatabaseAccess.Configurations;

public class AdminConfiguration : IEntityTypeConfiguration<Admin> {
	
	public void Configure(EntityTypeBuilder<Admin> builder) {
		builder.HasKey(x => x.AdminId);
		builder.Property(x => x.Email).IsRequired();
		builder.Property(x => x.Password).IsRequired();
	}
	
}