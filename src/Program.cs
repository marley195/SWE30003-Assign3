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

            bool exit = false;
            string option;
            List<Customer> Customers = new List<Customer>();
            // Create sample data
            Customer customer1 = new Customer("John Doe", "555-1234");
            Order order = new Order(customer1.CustomerID);
            var item1 = new MenuItem(10, "Pasta");
            var item2 = new MenuItem(15, "Pizza");
            menu.AddMenuItem(item1);
            menu.AddMenuItem(item2);
            Customers.Add(customer1);
            order.AddItem(item1);
            customer1.AddOrder(order);
            
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
                Console.WriteLine("3. Show Menu");
                Console.WriteLine("4. Place an order");
                Console.WriteLine("5. Display Tables");
                Console.WriteLine("6. List Available Tables");
                Console.WriteLine("7. Find Available Table");
                Console.WriteLine("8. Pay For An Order");
                Console.WriteLine("9. Display Menu");
                Console.WriteLine("10. Exit");
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
                            menu.DisplayMenu();
                            Console.Write("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }
                    case "4":
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
                    case "5":
                        tableManager.DisplayTables();
                        Console.Write("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "6":
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
                    case "7":
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
                    case "8":
                        {
                            //customer.Pay();
                            Console.Write("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }
                    case "9":
                        {
                            Console.WriteLine("checkcheck");
                            menu.DisplayMenu();
                            Console.Write("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }
                    case "10":
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


