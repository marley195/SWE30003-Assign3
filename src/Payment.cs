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
            AmountToPay = invoice.InvoiceTotal;
            PaymentDate = DateTime.Now;
            Status = PaymentStatus.Pending;

        }
        public void DisplayPaymentDetails()
        {
            Console.WriteLine($"Payment ID: {PaymentId}, Invoice ID: {InvoiceId}, Amount Paid: ${AmountToPay}, Payment Date: {PaymentDate}");
        }

        public void ProcessPayment(Invoice invoice)
        {
            Decimal PaymentRemaining = invoice.InvoiceTotal;
            Console.WriteLine($"---- Payment System ----");
            Console.WriteLine($"Payment ID: {PaymentId}, Invoice ID: {InvoiceId}, Amount Paid: ${AmountToPay}, Payment Date: {PaymentDate}");
            if (invoice.Status == Invoice.InvoiceStaus.Paid)
            {
                Console.WriteLine("Invoice has already been paid for.");
                return;
            }
            while(PaymentRemaining > 0)
            {
                Console.WriteLine($"Payment Remaining: {PaymentRemaining}\nEnter Payment Amount: ");
                decimal InputAmount = Convert.ToDecimal(Console.ReadLine());
                PaymentRemaining -= InputAmount;
                if (invoice.InvoiceTotal > PaymentRemaining)
                {
                    Console.WriteLine("Payment successfull for $" + InputAmount );
                }
                else if (PaymentRemaining == 0)
                {
                    Console.WriteLine("Processing payment amount of $" + InputAmount);
                    Status = PaymentStatus.Completed;
                    Console.WriteLine("Payment successful");
                }
                if (PaymentRemaining < 0)
                {
                    Console.WriteLine("Change Provide to Customer: $" + Math.Abs(PaymentRemaining));
                    Status = PaymentStatus.Completed;
                }
            }
        }
    }
}