namespace RelaxingKoala
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int InvoiceId { get; set; }
        public decimal AmountToPay { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus Status { get; set; }
        private static int nextPaymentId = 1;
        public enum PaymentStatus
        {
            Pending,
            Completed,
            Failed
        }

        public Payment(Invoice invoice)
        {
            PaymentId = nextPaymentId++;
            InvoiceId = invoice.InvoiceId;
            AmountToPay = invoice.AmountDue;
            PaymentDate = DateTime.Now;
            Status = PaymentStatus.Pending;

        }
        public void DisplayPaymentDetails()
        {
            Console.WriteLine($"Payment ID: {PaymentId}, Invoice ID: {InvoiceId}, Amount Paid: ${AmountToPay}, Payment Date: {PaymentDate}");
        }

        public void ProcessPayment(Invoice invoice)
        {
            Console.WriteLine($"---- Payment System ----");
            if (invoice.Status == Invoice.InvoiceStaus.Paid)
            {
                Console.WriteLine("Invoice has already been paid for.");
                return;
            }
            Console.WriteLine($"Amount due: {invoice.AmountDue}\nEnter Payment Amount: ");
            decimal InputAmount = Convert.ToDecimal(Console.ReadLine());
            if (invoice.AmountDue > AmountToPay)
            {
                Console.WriteLine("Payment unsuccessful. Amount paid is less than amount due.");
                Status = PaymentStatus.Failed;
                return;
            }
            else
            {
                Console.WriteLine($"Payment ID: {PaymentId}, Invoice ID: {InvoiceId}, Amount Paid: ${AmountToPay}, Payment Date: {PaymentDate}");
                Console.WriteLine("Processing payment...");
                Status = PaymentStatus.Completed;
                Console.WriteLine("Payment successful");
            }

        }
    }
}