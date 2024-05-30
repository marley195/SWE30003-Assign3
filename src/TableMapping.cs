using CsvHelper.Configuration;
using System.Linq;
using CsvHelper;
namespace RelaxingKoala
{
    public class TableMap : ClassMap<Table>
    {
        public TableMap()
        {
            Map(m => m.TableStatus).Name("TableStatus");
            Map(m => m.TableID).Name("TableID");
            Map(m => m.Capacity).Name("Capacity");
        }
    }
}