using CsvHelper.Configuration;
namespace RelaxingKoala
{
    

public sealed class TableMap : ClassMap<Table>
{
    public TableMap()
    {
        Map(m => m.TableID).Name("TableID");
        Map(m => m.TableStatus).Name("TableStatus");
        Map(m => m.Capacity).Name("Capacity");
    }
}
}