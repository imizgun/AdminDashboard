namespace AdminDashboardServer.DTO;

public class ClientDto {
	public Guid ClientId { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public double BalanceT { get; set; }
}