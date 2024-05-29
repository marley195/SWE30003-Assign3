using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace RelaxingKoala
{
    public class TableManger
    {
        private List<Table> tables;

        public TableManger() {
            tables = new List<Table>();
        }
        // Add table to the dictionary of tables
        public void CreateTable(int tableID, int capacity)
        {
            tables.Add(new Table(tableID, capacity));
            writeTables();
        }
                // Remove table from the dictionary of tables   
        public void RemoveTable(Table table)
        {
            tables.Remove(table);
        }

        public void writeTables()
        {
            using (var writer = new StreamWriter("tables.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<TableMap>();
                csv.WriteHeader<Table>();
                csv.NextRecord();
                foreach(var table in tables)
                {
                    csv.WriteRecord(table);
                    csv.NextRecord();
                }
            }
        }

        public void readTables()
        {
            if(!File.Exists("tables.csv"))
            {
                return;
            }
            using (var reader = new StreamReader("tables.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HeaderValidated = null,
                    MissingFieldFound = null
                };
                csv.Context.RegisterClassMap<TableMap>();
                tables = csv.GetRecords<Table>().ToList();
            }
        }

        public void UpdateTableFile(Table table)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
            using (var stream = File.Open("tables.csv", FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(tables);
            }
        }
        //Find the first available table with the capacity to accommodate the number of guests.
        public Table? FindAvailableTable(int capacity, DateTime dateTime)
        {
            return tables.FirstOrDefault(table => table.TableStatus == Table.Status.Available && table.Capacity >= capacity && table.TimeSlots.Any(TimeSlot => TimeSlot.StartTime <= dateTime && dateTime <= TimeSlot.EndTime && TimeSlot.BookingStatus == Table.Status.Available));
        }

        //Reserve table based on capacity
        public Table? ReserveTable(int capacity, DateTime dateTime)
        {
            Table? table = FindAvailableTable(capacity, dateTime);
            if(table != null)
            {
                table.TableStatus = Table.Status.Reserved;
                return table;
            }
            return null;
        }

        //Release Table based on TableID.
        public void ReleaseTable(Table table)
        {
            if(table != null)
            {
                table.TableStatus = Table.Status.Available;
            }
            else
            {
                Console.WriteLine("Table not found");
            }
        }

        // Display all Tables where TableID, Capacity and TableStatus are shown. - For Staff
        public void DisplayTables()
        {
            foreach (var table in tables)
            {
                Console.WriteLine($"Table ID: {table.TableID}, Capacity: {table.Capacity}, Status: {table.TableStatus}");
                foreach (var TimeSlot in table.TimeSlots)
                {
                    Console.WriteLine($"   Time: {TimeSlot.StartTime}, Status: {TimeSlot.BookingStatus}");
                }
            }
        }

        //return a list of all available tables.
        public List<Table> ListAvailableTables()
        {
            return tables.Where(table => table.TableStatus == Table.Status.Available).ToList();
        } 
        //Return available tables at Certain time 
        public List<Table> ListAvailableTables(DateTime dateTime)
        {
            return tables.Where(table => table.TableStatus == Table.Status.Available && table.TimeSlots.Any(TimeSlot => TimeSlot.StartTime == dateTime)).ToList();
        }
    }
}