using System.Collections.Generic;

namespace RelaxingKoala
{
       public class Order : ISubject
    {
        private List<IObserver> observers;
        public enum State {OrderPlaced, OrderProcessed, OrderCompleted, OrderCancelled};
        public Order(int orderId, int customerId)
        {
            OrderId = orderId;
            CustomerId = customerId;
            observers = new List<IObserver>();
            OrderState = State.OrderPlaced;
            AmountOwed = 0;
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


        public State OrderState {get; set;}

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public List<MenuItem> Items { get; set; } = new List<MenuItem>();
        public decimal TotalAmount => Items.Sum(item => item.Price);
        public decimal AmountOwed { get; set; }

        public void AddItem(MenuItem item)
        {
            Items.Add(item);
            AmountOwed += item.Price;
        }

        public void Pay(amount)
        {
            AmountOwed -= amount;
            if(AmountOwed < 0)
            {
                decimal extraAmount = AmountOwed * -1;
                AmountOwed = 0;
                Console.WriteLine($"The price has been overpayed. ${extraAmount} has been returned to you.");
            }
            CreateInvoice(AmountOwed);
        }

        public void CreateInvoice(amountOwed)
        {
            Invoice invoice = new(Invoice(invoiceID, OrderId, amountOwed, DateTime.Today));
            invoice.DisplayInvoiceDetails();
        }
    }
}
