using Microsoft.AspNetCore.Identity;

namespace AdminDashboardServer;

public class Hasher {
	public PasswordHasher<object> PasswordHasher { get; set; } =  new PasswordHasher<object>();
}