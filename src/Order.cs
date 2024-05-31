namespace RelaxingKoala
{
    public class Order : ISubject
    {
        private List<IObserver> observers;
        private int AmountOwed = 0;
        private static int nextorderId = 0;
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public State OrderState { get; set; }
        public List<MenuItem> Items { get; set; } = new List<MenuItem>();
        public int TotalAmount => Items.Sum(item => item.Price);
        public enum State { OrderPlaced, OrderProcessed, OrderCompleted, OrderCancelled };


        public Order( int customerId)
        {
            OrderId = nextorderId++;
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

        public void AddItem(MenuItem item)
        {
            Items.Add(item);
            AmountOwed += item.Price;
        }

        public Payment Pay()
        {
            Invoice invoice = this.CreateInvoice();
            this.OrderState = State.OrderCompleted;
            return invoice.pay();
        }

        public Invoice CreateInvoice()
        {
            Console.WriteLine($"Invoice has been created for OrderID: {this.OrderId}");
            return new Invoice(this);
        }

        public int getAmountOwed
        {
            get { return AmountOwed; }
        }
    }
}