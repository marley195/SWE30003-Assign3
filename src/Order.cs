using System.Collections.Generic;
using System.Security.Cryptography;

namespace RelaxingKoala
{
    public class Order : ISubject
    {
        private List<IObserver> observers;
        private int AmountOwed = 0;
        public enum State { OrderPlaced, OrderProcessed, OrderCompleted, OrderCancelled };
        public Order(int orderId, int customerId)
        {
            OrderId = orderId;
            CustomerId = customerId;
            observers = new List<IObserver>();
            OrderState = State.OrderPlaced;
        }

        public void AttachObserver(IObserver observer)
        {
            observers.Add(observer);
        }
        public void DetachObserver(IObserver observer)
        {
            observers.Remove(observer);
        }
        public void NotifyObserver()
        {
            foreach (var observer in observers)
            {
                observer.Update(this);
            }
        }


        public State OrderState { get; set; }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public List<MenuItem> Items { get; set; } = new List<MenuItem>();
        public decimal TotalAmount => Items.Sum(item => item.Price);

        public void AddItem(MenuItem item)
        {
            Items.Add(item);
            AmountOwed += item.Price;
        }

        public void Pay(int amount)
        {
            AmountOwed -= amount;
            if (AmountOwed < 0)
            {
                decimal extraAmount = AmountOwed * -1;
                AmountOwed = 0;
                Console.WriteLine($"The price has been overpayed. ${extraAmount} has been returned to you.");
            }
            CreateInvoice();
        }

        public void CreateInvoice()
        {
            int invoiceID = RandomNumberGenerator.GetInt32(2, 10);
            Invoice invoice = new Invoice(invoiceID, OrderId, AmountOwed);
            invoice.DisplayInvoiceDetails();
        }
        public int getAmountOwed
        {
            get { return AmountOwed; }
        }
    }
}