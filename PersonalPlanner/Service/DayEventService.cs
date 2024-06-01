using Calendar.Data;
using Calendar.IService;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

		public string Delete(int id)
		{
			string message = "";
			try
			{
				_oDayEvent = new DayEvent()
				{
					DayEventId = id
				};

				IDbConnection con = new SqlConnection(Configuration.GetConnectionString("MyCalendar"));

				if (con.State == ConnectionState.Closed) con.Open();

				var oDayEvents = con.Query<DayEvent>("SP_DayEvent", this.SetParameters(_oDayEvent, (int)OperationType.Delete), commandType: CommandType.StoredProcedure);

				message = "Deleted";
				
				con.Close();
			}
			catch (Exception ex)
			{
				message = ex.Message;
			}

			return message;
		}

		public DayEvent GetEvent(DateTime eventDate)
		{
			_oDayEvent = new DayEvent();
			IDbConnection con = new SqlConnection(Configuration.GetConnectionString("MyCalendar"));
			if (con.State == ConnectionState.Closed) con.Open();

			string sql = string.Format(@"SELECT * FROM DayEvent WHERE EventDate = '{0}'", eventDate.ToString("dd-MMM-yyyy"));

			var oDayEvents = con.Query<DayEvent>(sql).ToList();

			if (oDayEvents != null && oDayEvents.Count() > 0)
			{
				_oDayEvent = oDayEvents.SingleOrDefault();
			}
			else
			{
				_oDayEvent.EventDate = eventDate;
				_oDayEvent.FromDate = eventDate;
				_oDayEvent.ToDate= eventDate;

			}

			con.Close();
			return _oDayEvent;
		}

		public List<DayEvent> GetEvents(DateTime fromDate, DateTime toDate)
		{
			_oDayEvents = new List<DayEvent>();

			IDbConnection con = new SqlConnection(Configuration.GetConnectionString("MyCalendar"));
			if (con.State == ConnectionState.Closed) con.Open();

			string sql = string.Format(@"SELECT * FROM DayEvent WHERE EventDate BETWEEN '{0}' AND '{1}'", fromDate.ToString("dd-MMM-yyyy"), toDate.ToString("dd-MMM-yyyy"));

			var oDayEvents = con.Query<DayEvent>(sql).ToList();

			if (oDayEvents != null && oDayEvents.Count() > 0)
			{
				_oDayEvents = oDayEvents;
			}

			con.Close();

			return _oDayEvents;
		}

		public DayEvent SaveOrUpdate(DayEvent oDayEvent)
		{
			_oDayEvent = new DayEvent();
			try
			{
				int operationType = Convert.ToInt32(oDayEvent.DayEventId == 0 ? OperationType.Insert : OperationType.Update);

				IDbConnection con = new SqlConnection(Configuration.GetConnectionString("MyCalendar"));
				if (con.State == ConnectionState.Closed) con.Open();

				var oDayEvents = con.Query<DayEvent>("SP_DayEvent", this.SetParameters(oDayEvent, operationType), commandType: CommandType.StoredProcedure);

				if (oDayEvents != null && oDayEvents.Count() > 0)
				{
					_oDayEvent = oDayEvents.FirstOrDefault();
				}

				con.Close();
			}
			catch (Exception ex)
			{
				_oDayEvent.Message = ex.Message;
			}
			return _oDayEvent;
		}

		private DynamicParameters SetParameters(DayEvent oDayEvent, int operationType)
		{
			DynamicParameters parameters = new DynamicParameters();

			parameters.Add("@DayEventId", oDayEvent.DayEventId);
			parameters.Add("@Note", oDayEvent.Note);
			parameters.Add("@EventDate", oDayEvent.EventDate);
			parameters.Add("@OperationType", operationType);

			return parameters;
		}
	}
}
