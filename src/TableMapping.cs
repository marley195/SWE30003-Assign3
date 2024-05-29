using CsvHelper.Configuration;
using System.Linq;
using CsvHelper;
namespace RelaxingKoala
{
    public class TableMap : ClassMap<Table>
    {
        public TableMap()
        {
            Map(m => m.TableID).Name("TableID");
            Map(m => m.Capacity).Name("Capacity");
            Map(m => m.TimeSlots).Convert(row =>
            {
                return string.Join(", ", row.Value);
            }).Name("TimeSlot");
        }
    }

    public class TimeSlotMap : ClassMap<TimeSlot>
    {
        public TimeSlotMap()
        {
            Map(m => m.StartTime).TypeConverterOption.Format("HH:mm").Name("StartTime");
            Map(m => m.EndTime).TypeConverterOption.Format("HH:mm").Name("EndTime");
            Map(m => m.BookingStatus).Name("BookingStatus");
        }
    }
}
