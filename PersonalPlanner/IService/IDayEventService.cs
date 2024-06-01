using Calendar.Data;

namespace Calendar.IService
{
	public interface IDayEventService
	{
		DayEvent SaveOrUpdate(DayEvent oDayEvent);
		DayEvent GetEvent(DateTime eventDate);

		List<DayEvent> GetEvents(DateTime fromDate, DateTime toDate);

		string Delete(int id);
	}
}
