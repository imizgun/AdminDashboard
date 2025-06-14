export interface IPayment {
    paymentId: string;
    senderId: string;
    senderEmail: string;
    receiverId: string;
    receiverEmail: string;
    amount: number;
    paymentDate: string;
}