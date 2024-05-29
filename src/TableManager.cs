using System.Globalization;
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
        //Find the first available table with the capacity to accommodate the number of guests.
        public Table? FindAvailableTable(int capacity)
        {
            return tables.FirstOrDefault(table => table.TableStatus == Table.Status.Available && table.Capacity >= capacity);
        }
        //Reserve table based on capacity
        public Table? ReserveTable(int capacity)
        {
            Table? table = FindAvailableTable(capacity);
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
            }
        }

        //return a list of all available tables.
        public List<Table> ListAvailableTables()
        {
            return tables.Where(table => table.TableStatus == Table.Status.Available).ToList();
        } 
        
    }
}