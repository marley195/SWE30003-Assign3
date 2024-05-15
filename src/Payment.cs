namespace RelaxingKoala
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int InvoiceId { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }

        public Payment(int paymentId, int invoiceId, decimal amountPaid)
        {
            PaymentId = paymentId;
            InvoiceId = invoiceId;
            AmountPaid = amountPaid;
            PaymentDate = DateTime.Now;
        }
        public void DisplayPaymentDetails()
        {
            Console.WriteLine($"Payment ID: {PaymentId}, Invoice ID: {InvoiceId}, Amount Paid: ${AmountPaid}, Payment Date: {PaymentDate}");
        }
    }
}