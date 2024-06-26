namespace RelaxingKoala
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int OrderId { get; set; }
        public decimal InvoiceTotal { get; set; }
        public DateTime InvoiceDate { get; set; }
        public InvoiceStaus Status { get; set; }
        private static int invoiceNumber = 0;


        public Invoice(Order order)
        {
            InvoiceId = invoiceNumber++;
            OrderId = order.OrderId;
            InvoiceTotal = order.TotalAmount;
            InvoiceDate = DateTime.Now;
            Status = InvoiceStaus.Unpaid;
            
        }
        public void DisplayInvoiceDetails()
        {
            Console.WriteLine($"Invoice ID: {InvoiceId}, Order ID: {OrderId}, Amount Due: ${InvoiceTotal}, Date Issued: {InvoiceDate.ToShortDateString()}");
        }
        
        public Payment pay()
        {
            Payment payment = new Payment(this);
            payment.ProcessPayment(this);
            this.Status = InvoiceStaus.Paid;
            return payment;
        }

        public enum InvoiceStaus
        {
            Unpaid,
            Paid
        }
    }
}