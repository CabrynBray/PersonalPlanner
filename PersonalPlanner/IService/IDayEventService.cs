using Calendar.Data;

namespace Calendar.IService
{
	public interface IDayEventService
	{
		public Task DeleteDayEvent(DayEvent dayEvent);

		public Task<DayEvent?> GetAsync(DateTime eventDate);

		public Task<List<DayEvent>> GetEvents(DateTime fromDate, DateTime toDate);
			
		public Task<DayEvent> CreateAsync(DayEvent dayEvent);
	}
}
