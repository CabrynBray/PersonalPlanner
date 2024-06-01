using Calendar.Data;

namespace Calendar.IService
{
	public interface IHourEventService
	{
		HourEvent SaveOrUpdate(HourEvent oHourEvent);
		HourEvent GetEvent(DateTime eventDate, string hour);

		List<HourEvent> GetEvents(DateTime eventDate);

		List<HourEvent> GetEvents(DateTime fromDate, DateTime toDate);

		string Delete(int id);
	}
}
