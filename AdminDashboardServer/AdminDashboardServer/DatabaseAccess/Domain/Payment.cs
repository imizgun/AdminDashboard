namespace AdminDashboardServer.DatabaseAccess.Domain;

public class Payment {
	public Guid PaymentId { get; set; }
	
	public Guid SenderId  { get; set; }
	public Client Sender { get; set; }
	
	public Guid ReceiverId  { get; set; }
	public Client Receiver { get; set; }
	
	public DateTime PaymentDate { get; set; }
	public double Amount { get; set; }

	public Payment() {}
	private Payment(Guid paymentId, Guid senderId, Guid receiverId, double amount, DateTime paymentDate) {
		PaymentId = paymentId;
		SenderId = senderId;
		ReceiverId = receiverId;
		PaymentDate = paymentDate;
		Amount = amount;
	}

	public static Payment CreateNew(Guid senderId, Guid receiverId, double amount, DateTime paymentDate) {

		return new Payment(Guid.NewGuid(), senderId, receiverId, amount, paymentDate);
	}
}