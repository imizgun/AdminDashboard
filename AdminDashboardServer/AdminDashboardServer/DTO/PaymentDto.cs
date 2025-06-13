namespace AdminDashboardServer.DTO;

public class PaymentDto {
	public Guid PaymentId { get; set; }
	public Guid SenderId  { get; set; }
	public string SenderEmail { get; set; }
	public Guid ReceiverId  { get; set; }
	public string ReceiverEmail { get; set; }
	public DateTime PaymentDate { get; set; }
	public double Amount { get; set; }
}