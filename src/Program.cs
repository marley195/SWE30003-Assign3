using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace RelaxingKoala
{
    class Program
    {
        public static void Main()
        {
            //Object creation
            ReservationManager reservationManager = new ReservationManager();
            TableManger tableManager = new TableManger();
            bool exit = false;
            string option;
            // Create sample data
            var customer = new Customer("John Doe", "555-1234");
            var order = new Order (1,1);
            var menuItem = new MenuItem { MenuItemId = 1, Name = "Pizza", Price = 15.99M };
            //Temporary to Generate Tables for testing
            for (int i = 0; i < 10; i++)
            {
                tableManager.AddTable(i, RandomNumberGenerator.GetInt32(1, 5));
            }
            //Loop for Reservation + table system.
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Relaxing Koala!");
                Console.WriteLine("Please select an option:");
                Console.WriteLine("1. Reserve Table");
                Console.WriteLine("2. Display Tables");
                Console.WriteLine("3. List Available Tables");
                Console.WriteLine("4. Find Available Table");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");
                option = Console.ReadLine();
                switch(option)
                {
                case "1":
                {
                    Console.Write("Enter number of guests: ");
                    int guests = int.Parse(Console.ReadLine());
                    Table table = tableManager.FindAvailableTable(guests);
                    if (table != null)
                    {   
                        Console.WriteLine("Table found\nEnter you name: ");
                        string? name = Console.ReadLine();
                        Console.WriteLine("Enter your contact number: ");
                        string? contact = Console.ReadLine();
                        customer = new Customer(name, contact, "No requirements");
                        reservationManager.CreateReservation(customer.CustomerId, table, DateTime.Now.AddHours(2), guests);
                        tableManager.ReserveTable(table.TableID);
                        Console.WriteLine($"Table {table.TableID} reserved for {guests} guests");
                    }
                    else
                    {
                        Console.WriteLine("No tables available");
                    }
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                    break;
                }
                case "2":
                    tableManager.DisplayTables();
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                    break;
                case "3":
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
                case "4":
                {
                    Console.Write("Enter number of guests: ");
                    int guests = int.Parse(Console.ReadLine());
                    Table? availableTable = tableManager.FindAvailableTable(guests);
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
                case "5":
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


