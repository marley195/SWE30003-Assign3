namespace RelaxingKoala
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
        public List<Order> Orders { get; set; } = new List<Order>();

        // Constructor to initialize a new customer with essential details
        public Customer(int id, string name, string contactNumber)
        {
            CustomerId = id;
            Name = name;
            ContactNumber = contactNumber;
        }

        // Method to add a reservation for the customer
        public void AddReservation(Reservation reservation)
        {
            Reservations.Add(reservation);
        }

        // Method to add an order for the customer
        public void AddOrder(Order order) => Orders.Add(order);

        // Method to display customer details along with reservations and orders
        public void DisplayCustomerDetails()
        {
            Console.WriteLine($"Customer ID: {CustomerId}, Name: {Name}, Contact: {ContactNumber}");
            Console.WriteLine("Reservations:");
            foreach (var res in Reservations)
            {
                Console.WriteLine($"   Reservation at {res.ReservationTime}, Table ID: {res.TableId}, Guests: {res.NumberOfGuests}");
            }
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