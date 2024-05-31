using System.Runtime.InteropServices;
using System.Security.Cryptography;
using CsvHelper;

namespace RelaxingKoala
{
    class Program
    {

        public static void Main()
        {
            //Object creation
            ReservationManager reservationManager = new ReservationManager();
            TableManager tableManager = new TableManager();
            Menu menu = new Menu();
            Kitchen Kitchen = new Kitchen();

            bool exit = false;
            string option;
            List<Customer> Customers = new List<Customer>();
            // Create sample data
            menu.AddMenuItem(new MenuItem(10, "Burger", MenuItem.FoodCategory.MainCourse));
            menu.AddMenuItem(new MenuItem(5, "Fries", MenuItem.FoodCategory.Appetizer));
            menu.AddMenuItem(new MenuItem(15, "Steak", MenuItem.FoodCategory.MainCourse));
            menu.AddMenuItem(new MenuItem(5, "Salad", MenuItem.FoodCategory.Appetizer));
            menu.AddMenuItem(new MenuItem(5, "Ice Cream", MenuItem.FoodCategory.Dessert));
            menu.AddMenuItem(new MenuItem(5, "Soda", MenuItem.FoodCategory.Drink));
            menu.AddMenuItem(new MenuItem(15, "Wine", MenuItem.FoodCategory.Drink));
            Customers.Add(new Customer("John Doe", "555-1234"));
            //Temporary to Generate Tables for testing
            for (int i = 0; i < 10; i++)
            {
                tableManager.CreateTable(i, RandomNumberGenerator.GetInt32(2, 10));
            }
            //tableManager.readTables();

            //Loop for Reservation + table system.
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Relaxing Koala!");
                Console.WriteLine("Please select an option:");
                Console.WriteLine("1. Show Customer Details");
                Console.WriteLine("2. Reserve Table");
                Console.WriteLine("3. Place an order");
                Console.WriteLine("4. Display Tables");
                Console.WriteLine("5. List Available Tables");
                Console.WriteLine("6. Find Available Table");
                Console.WriteLine("7. Pay For An Order");
                Console.WriteLine("8. Display Menu");
                Console.WriteLine("9. Exit");
                Console.Write("Select an option: ");
                option = Console.ReadLine() ?? "";
                switch (option)
                {
                    case "1":
                        {
                            foreach(Customer customer1 in Customers)
                            {
                                {
                                    customer1.DisplayCustomerDetails(reservationManager);

                                }
                            }
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }
                    case "2":
                        {
                            Console.Write("Enter number of guests: ");
                            int guests = int.Parse(Console.ReadLine() ?? "");
                            Table? table = tableManager.FindAvailableTable(guests, DateTime.Today);
                            if (table != null)
                            {
                                Console.WriteLine("Table found\nEnter you name: ");
                                string name = Console.ReadLine() ?? "";
                                Console.WriteLine("Enter your contact number: ");
                                string contact = Console.ReadLine() ?? "";
                                Customers.Add(new Customer(name, contact, "No requirements"));
                                reservationManager.CreateReservation(Customers[Customers.Count-1], tableManager, DateTime.Now.AddHours(2), guests);
                            }
                            else
                            {
                                Console.WriteLine("No tables available");
                            }
                            Console.Write("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }
                    case "3":
                        {
                            Console.Write("Enter number of guests: ");
                            int guest = int.Parse(Console.ReadLine() ?? "");
                            Table? _table = tableManager.FindAvailableTable(guest, DateTime.Today);
                            if (_table != null)
                            {
                                Console.WriteLine("Table found\nEnter you name: ");
                                string name = Console.ReadLine() ?? "";
                                Console.WriteLine("Enter your contact number: ");
                                string contact = Console.ReadLine() ?? "";
                                Customers.Add(new Customer(name, contact, "No requirements"));
                                reservationManager.OccupyTable(Customers[Customers.Count-1], tableManager, DateTime.Now, guest);

                                //take order
                                Order order = new Order(Customers[Customers.Count-1].CustomerId);
                                bool ordering = true;
                                while (ordering)
                                {
                                    Console.Clear();
                                    menu.DisplayMenu();
                                    Console.Write("Enter the Menu Item ID to add to order, or 'done' to finish: ");
                                    string menuItemInput = Console.ReadLine() ?? "";

                                    if (menuItemInput.ToLower() == "done")
                                    {
                                        Customers[Customers.Count-1].AddOrder(order);
                                        Kitchen.Update(order);
                                        ordering = false;
                                        
                                    }
                                    else if (int.TryParse(menuItemInput, out int menuItemId))
                                    {
                                        MenuItem? menuItem = menu.MenuItems.FirstOrDefault(item => item.MenuItemId == menuItemId);
                                        if (menuItem != null)
                                        {
                                            order.AddItem(menuItem);
                                            Console.WriteLine($"{menuItem.Name} added to order.");
                                            Console.Write("Press any key to continue...");
                                            Console.ReadKey();

                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid Menu Item ID.");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("No tables available");
                            }
                            Console.Write("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }
                    case "4":
                        tableManager.DisplayTables();
                        Console.Write("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "5":
                        {
                            var availableTables = tableManager.ListAvailableTables();
                            if (availableTables.Count > 0)
                            {
                                Console.WriteLine("Available Tables:");
                                foreach (var availableTable in availableTables)
                                {
                                    Console.WriteLine($"Table ID: {availableTable.TableID}, Capacity: {availableTable.Capacity}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No tables available");
                            }
                            Console.Write("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }
                    case "6":
                        {
                            Console.Write("Enter number of guests: ");
                            int guests = int.Parse(Console.ReadLine() ?? "");
                            Table? availableTable = tableManager.FindAvailableTable(guests, DateTime.Today);
                            if (availableTable != null)
                            {
                                Console.WriteLine($"Table ID: {availableTable.TableID}, Capacity: {availableTable.Capacity}");
                            }
                            else
                            {
                                Console.WriteLine("No tables available");
                            }
                            Console.Write("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }
                    case "7":
                        {
                            Console.WriteLine($"Select a customer to pay for an order: {String.Join(", ", Customers.Select(c => c.Name))}");
                            string customerName = Console.ReadLine() ?? "";
                            Customer? customer = Customers.FirstOrDefault(c => c.Name.Equals(customerName, StringComparison.OrdinalIgnoreCase));
                            if (customer != null)
                            {
                                customer.DisplayOrders();
                                Console.WriteLine("Please select the order number you wish to pay for:");
                                if (int.TryParse(Console.ReadLine() ?? "0", out int selectedOrder))
                                {
                                    Order? requiredOrder = customer.Orders.FirstOrDefault(order => order.OrderId == selectedOrder);

                                    if (requiredOrder == null)
                                    {
                                        Console.WriteLine("That order could not be found");
                                    }
                                    else
                                    {
                                        customer.Pay(requiredOrder);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("A valid order ID was not submitted.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Customer not found");
                            }
                            Console.Write("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }
                    case "8":
                        {
                            Console.WriteLine("checkcheck");
                            menu.DisplayMenu();
                            Console.Write("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }
                    case "9":
                        {
                            exit = true;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid option");
                            Console.Write("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }
                }
            }
        }
    }
}


