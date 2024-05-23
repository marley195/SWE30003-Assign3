using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using CsvHelper;
using CsvHelper.Configuration;

namespace RelaxingKoala
{
    public class TableManger
    {
        private List<Table> tables;

        public TableManger()
        {
            tables = new List<Table>();
        }
        // Add table to the dictionary of tables
        public void AddTable(int tableID, int capacity)
        {
            Table table = new Table(tableID, capacity);
            tables.Add(table);
            writeTables();
        }

        public void writeTables()
        {
            using (var writer = new StreamWriter("tables.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(tables);
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
        // Remove table from the dictionary of tables   
        public void RemoveTable(Table table)
        {
            tables.Remove(table);
        }
        //Return a table by its TableID
        public Table GetTableByID(int tableID)
        {
            return tables[tableID];
        }
        public Table? GetTableByCapacity(int capacity)
        {
                return tables.First(table => table.Capacity == capacity);
        }

        // Display all Tables where TableID, Capacity and TableStatus are shown. - For Staff
        public void DisplayTables()
        {
            foreach (var table in tables)
            {
                Console.WriteLine($"Table ID: {table.TableID}, Capacity: {table.Capacity}, Status: {table.TableStatus}");
            }
        }
        //Find the first available table with the capacity to accommodate the number of guests.
        public Table? FindAvailableTable(int capacity)
        {
            return tables.FirstOrDefault(table => table.TableStatus == Table.Status.Available && table.Capacity >= capacity);
        }
        //Reserve Table based on TableID.
        public void ReserveTable(Table table)
        {
            if(table != null)
            {
                table.TableStatus = Table.Status.Reserved;
            }
            else
            {
                Console.WriteLine("Table not found");
            }
        }
        //Change Table status to Occupied based on TableID.
        public void OccupyTable(Table table)
        {
            if(table != null)
            {
                table.TableStatus = Table.Status.Occupied;
            }
            else
            {
                Console.WriteLine("Table not found");
            }
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
        //return a list of all available tables.
        public List<Table> ListAvailableTables()
        {
            return tables.Where(table => table.TableStatus == Table.Status.Available).ToList();
        } 
        
    }
}