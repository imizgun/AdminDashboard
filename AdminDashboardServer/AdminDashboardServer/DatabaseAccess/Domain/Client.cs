using System.Runtime.CompilerServices;

namespace AdminDashboardServer.DatabaseAccess.Domain;

public class Client {
	public Guid ClientId { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public double BalanceT { get; set; }
	public List<Payment> ClientPayments { get; set; }
	public List<Payment> ClientIncomings { get; set; }
	
	public Client() {}

	private Client(Guid clientId, string name, string email, double balanceT, List<Payment> clientPayments, List<Payment> clientIncomings) {
		ClientId = clientId;
		Name = name;
		Email = email;
		BalanceT = balanceT;
		ClientPayments = clientPayments;
		ClientIncomings = clientIncomings;
	}

	public static Client CreateNew(string name, string email, double balanceT, List<Payment> clientPayments,
		List<Payment> clientIncomings) {
		
		return new Client(
			Guid.NewGuid(), 
			name, 
			email, 
			balanceT, 
			clientPayments, 
			clientIncomings
			);
		
	}
}