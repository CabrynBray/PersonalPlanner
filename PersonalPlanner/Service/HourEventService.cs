using Calendar.Data;
using Calendar.IService;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using PersonalPlanner.Data;
using MongoDB.Driver;

namespace Calendar.Service
{
	public class HourEventService : IHourEventService
	{

		HourEvent _oHourEvent = new HourEvent();
		List<HourEvent> _oHourEvents = new List<HourEvent>();

		public IConfiguration Configuration { get; }

		public HourEventService(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		MongoDBContext db = new MongoDBContext(new MongoDBSettings() {
			ConnectionString = "mongodb+srv://team04:X0QZDHtKPev5cv1B@team4cluster.hfsodnz.mongodb.net/",
			DatabaseName = "PersonalPlanner"
		});

		public async Task DeleteHourEvent(HourEvent hourEvent)
		{
			if (hourEvent == null) return;

			var filter = Builders<HourEvent>.Filter.Eq(g => g.HourEventId, hourEvent.HourEventId);
			var deleteResult = await db.HourEvent.DeleteOneAsync(filter);
		}

		public async Task<HourEvent?> GetAsync(DateTime eventDate) =>
        	await db.HourEvent.Find(x => x.HourEventDay == eventDate).FirstOrDefaultAsync();

		public async Task<List<HourEvent>> GetEvents(DateTime fromDate, DateTime toDate){
			var filterFrom = Builders<HourEvent>.Filter.Gte(g => g.HourEventDay, fromDate);
			var filterTo = Builders<HourEvent>.Filter.Lte(g => g.HourEventDay, toDate);
			return await db.HourEvent.Find(filterFrom&filterTo).ToListAsync();
		}

		public async Task<HourEvent> CreateAsync(HourEvent hourEvent) {
			if (hourEvent.HourEventId == 0) {
				var insert = db.HourEvent.InsertOneAsync(hourEvent);
			} else {
				var update = db.HourEvent.ReplaceOneAsync(x => x.HourEventId == hourEvent.HourEventId, hourEvent);
			}

			return await db.HourEvent.Find(x => x.HourEventId == hourEvent.HourEventId).FirstOrDefaultAsync();
			
		}
	}
}
