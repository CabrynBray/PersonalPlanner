using Calendar.Data;

namespace Calendar.IService
{
	public interface IHourEventService
	{
		public Task DeleteHourEvent(HourEvent hourEvent);

		public Task<HourEvent?> GetAsync(DateTime eventDate);

		public Task<List<HourEvent>> GetEvents(DateTime fromDate, DateTime toDate);

		public Task<HourEvent> CreateAsync(HourEvent hourEvent);
	}
}
