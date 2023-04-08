namespace Migartions.Models
{
    public class EventSportsman
    {
        public Guid EventId { get; set; }
        public Guid SportsmanId { get; set; }

        public int StreamNumber { get; set; }
    }
}
