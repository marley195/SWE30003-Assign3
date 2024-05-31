/* 
    Kitchen class is an observer class that implements IObserver interface.
    It has an Update method that takes an ISubject object as a parameter.
    It checks if the ISubject object is an instance of Order class.
    If it is, it casts the ISubject object to Order object and processes order as a Kitchen would.
    Update Order to new Order state and print the new Order state.
*/


namespace RelaxingKoala
{
    public class Kitchen : IObserver
    {
        public void Update(ISubject iSubject)
        {
            if (iSubject is Order)
            {
            Order order = (Order)iSubject;
            Console.WriteLine($"Kitchen received the order, Order status is now '{order.OrderState}'");
            order.OrderState = Order.State.OrderProcessed;
            Console.WriteLine($"Order status is now '{order.OrderState}'");
            }
        }
    }
}