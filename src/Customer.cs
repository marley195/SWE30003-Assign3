namespace RelaxingKoala
{
    public class Customer 
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Requirements { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();

        // Constructor to initialize a new customer with essential details
        public Customer(int id, string name, string contactNumber, string requirements = "")
        {
            CustomerId = id;
            Name = name;
            ContactNumber = contactNumber;
            Requirements = requirements;
        }

        // Method to add an order for the customer
        public void AddOrder(Order order) => Orders.Add(order);

        // Method to display customer details along with reservations and orders
        public void DisplayCustomerDetails(ReservationManager reservationManager)
        {
            Console.WriteLine($"Customer ID: {CustomerId}, Name: {Name}, Contact: {ContactNumber}");
            Console.WriteLine("Reservations:");
            Reservation Reservations = reservationManager.GetReservationByCustomer(CustomerId);
            Console.WriteLine($"   Reservation ID: {Reservations.ReservationId}, Table ID: {Reservations.TableId}, Time: {Reservations.ReservationTime}, Guests: {Reservations.NumberOfGuests}");
            Console.WriteLine("Orders:");
            foreach (var order in Orders)
            {
                Console.WriteLine($"   Order ID: {order.OrderId}, Total: ${order.TotalAmount}");
                foreach (var item in order.Items)
                {
                    Console.WriteLine($"      Item: {item.Name}, Price: ${item.Price}");
                }
            }
        }
    }
}