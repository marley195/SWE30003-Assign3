namespace RelaxingKoala
{
    public class TableManager
    {
        private List<Table> tables;

        public TableManager()
        {
            tables = new List<Table>();
        }
        // Add table to the dictionary of tables
        public void CreateTable(int tableID, int capacity)
        {
            tables.Add(new Table(tableID, capacity));
            //writeTables();
        }
        // Remove table from the dictionary of tables   
        public void RemoveTable(Table table)
        {
            tables.Remove(table);
        }

        //public void UpdateTableFile(Table table)
        //{
        //    writeTables();

        //}
        //Find the first available table with the capacity to accommodate the number of guests.
        public Table? FindAvailableTable(int capacity, DateTime dateTime)
        {
            return tables.FirstOrDefault(table => table.TableStatus == Table.Status.Available && table.Capacity >= capacity);
        }

        //Reserve table based on capacity
        public Table? ReserveTable(int capacity, DateTime dateTime)
        {
            Table? table = FindAvailableTable(capacity, dateTime);
            if (table != null)
            {
                table.TableStatus = Table.Status.Reserved;
                return table;
            }
            return null;
        }

        //Release Table based on TableID.
        public void ReleaseTable(Table table)
        {
            if (table != null)
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
        //Return available tables at Certain time 
        public List<Table> ListAvailableTables(DateTime dateTime)
        {
            return tables.Where(table => table.TableStatus == Table.Status.Available).ToList();
        }
    }
}