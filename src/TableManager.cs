using System.Security.Cryptography.X509Certificates;

namespace RelaxingKoala
{
    public class TableManger
    {
        private Dictionary<int, Table> tables;

        public TableManger()
        {
            tables = new Dictionary<int, Table>();
        }
        // Add table to the dictionary of tables
        public void AddTable(int tableID, int capacity)
        {
            Table table = new Table(tableID, capacity);
            tables.Add(tableID, table);
        }
        // Remove table from the dictionary of tables   
        public void RemoveTable(int tableID)
        {
            tables.Remove(tableID);
        }
        //Return a table by its TableID
        public Table GetTableByID(int tableID)
        {
            return tables[tableID];
        }
        // Display all Tables where TableID, Capacity and TableStatus are shown. - For Staff
        public void DisplayTables()
        {
            foreach (var table in tables)
            {
                Console.WriteLine($"Table ID: {table.Value.TableID}, Capacity: {table.Value.Capacity}, Status: {table.Value.TableStatus}");
            }
        }
        //Find the first available table with the capacity to accommodate the number of guests.
        public Table? FindAvailableTable(int capacity)
        {
            return tables.Values.FirstOrDefault(table => table.TableStatus == Table.Status.Available && table.Capacity >= capacity);
        }
        //Reserve Table based on TableID.
        public void ReserveTable(int tableID)
        {
            if(tables.ContainsKey(tableID))
            {
                tables[tableID].TableStatus = Table.Status.Reserved;
            }
            else
            {
                Console.WriteLine("Table not found");
            }
        }
        //Change Table status to Occupied based on TableID.
        public void OccupyTable(int tableID)
        {
            if(tables.ContainsKey(tableID))
            {
                tables[tableID].TableStatus = Table.Status.Occupied;
            }
            else
            {
                Console.WriteLine("Table not found");
            }
        }
        //Release Table based on TableID.
        public void ReleaseTable(int tableID)
        {
            if(tables.ContainsKey(tableID))
            {
                tables[tableID].TableStatus = Table.Status.Available;
            }
            else
            {
                Console.WriteLine("Table not found");
            }
        }
        //return a list of all available tables.
        public List<Table> ListAvailableTables()
        {
            return tables.Values.Where(table => table.TableStatus == Table.Status.Available).ToList();
        } 
        
    }
}