namespace AdminDashboardServer.DatabaseAccess.Domain;

public class Admin {
	public Guid AdminId { get; set; }
	public string Email {get; set;}
	public string Password {get; set;}
	
	public Admin() {}

	private Admin(Guid adminId, string email, string password) {
		AdminId = adminId;
		Email = email;
		Password = password;
	}

	public static Admin CreateAdmin(string email, string password) {
		return new Admin(Guid.NewGuid(), email, password);
	}
}