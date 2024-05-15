namespace RelaxingKoala.
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int OrderId { get; set; }
        public decimal AmountDue { get; set; }
        public DateTime DateIssued { get; set; }

        public Invoice(int invoiceId, int orderId, decimal amountDue)
        {
            InvoiceId = invoiceId;
            OrderId = orderId;
            AmountDue = amountDue;
            DateIssued = DateTime.Now;
        }
        public void DisplayInvoiceDetails()
        {
            Console.WriteLine($"Invoice ID: {InvoiceId}, Order ID: {OrderId}, Amount Due: ${AmountDue}, Date Issued: {DateIssued}");
        }
    }
}