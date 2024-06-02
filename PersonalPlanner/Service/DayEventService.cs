using Calendar.Data;
using Calendar.IService;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;
using PersonalPlanner.Data;
using MongoDB.Driver;

namespace Calendar.Service
{
	public class DayEventService : IDayEventService
	{
		DayEvent _oDayEvent = new DayEvent();
		List<DayEvent> _oDayEvents = new List<DayEvent>();

		public IConfiguration Configuration { get; }

		public DayEventService(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		MongoDBContext db = new MongoDBContext(new MongoDBSettings() {
			ConnectionString = "mongodb+srv://team04:X0QZDHtKPev5cv1B@team4cluster.hfsodnz.mongodb.net/",
			DatabaseName = "PersonalPlanner"
		});

		public async Task DeleteDayEvent(DayEvent dayEvent)
		{
			if (dayEvent == null) return;

			var filter = Builders<DayEvent>.Filter.Eq(g => g.DayEventId, dayEvent.DayEventId);
			var deleteResult = await db.DayEvent.DeleteOneAsync(filter);
		}

		public async Task<DayEvent?> GetAsync(DateTime eventDate) =>
        	await db.DayEvent.Find(x => x.EventDate == eventDate).FirstOrDefaultAsync();

		public async Task<List<DayEvent>> GetEvents(DateTime fromDate, DateTime toDate){
			var filterFrom = Builders<DayEvent>.Filter.Gte(g => g.EventDate, fromDate);
			var filterTo = Builders<DayEvent>.Filter.Lte(g => g.EventDate, toDate);
			return await db.DayEvent.Find(filterFrom&filterTo).ToListAsync();
		}
			
		public async Task<DayEvent> CreateAsync(DayEvent dayEvent) {
			if (dayEvent.DayEventId == 0) {
				var insert = db.DayEvent.InsertOneAsync(dayEvent);
			} else {
				var update = db.DayEvent.ReplaceOneAsync(x => x.DayEventId == dayEvent.DayEventId, dayEvent);
			}

			return await db.DayEvent.Find(x => x.DayEventId == dayEvent.DayEventId).FirstOrDefaultAsync();
			
		}
	}
}
