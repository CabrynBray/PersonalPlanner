using Calendar.Data;
using Calendar.IService;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

		public string Delete(int id)
		{
			string message = "";
			try
			{
				_oHourEvent = new HourEvent()
				{
					HourEventId = id
				};

				IDbConnection con = new SqlConnection(Configuration.GetConnectionString("MyCalendar"));

				if (con.State == ConnectionState.Closed) con.Open();

				var oHourEvents = con.Query<HourEvent>("SP_HourEvent", this.SetParameters(_oHourEvent, (int)OperationType.Delete), commandType: CommandType.StoredProcedure);

				message = "Deleted";

				con.Close();
			}
			catch (Exception ex)
			{
				message = ex.Message;
			}

			return message;
		}

		public HourEvent GetEvent(DateTime eventDate, string hour)
		{
			_oHourEvent = new HourEvent();
			IDbConnection con = new SqlConnection(Configuration.GetConnectionString("MyCalendar"));
			if (con.State == ConnectionState.Closed) con.Open();

			string sql = string.Format(@"SELECT * FROM HourEvent WHERE HourEventDay = '{0}'", eventDate.ToString("dd-MMM-yyyy"));

			var oHourEvents = con.Query<HourEvent>(sql).ToList();

			if (oHourEvents != null && oHourEvents.Count() > 0)
			{
				_oHourEvent = oHourEvents.SingleOrDefault();
			}
			else
			{
				_oHourEvent.HourEventDay = eventDate;

			}

			con.Close();
			return _oHourEvent;
		}

		public List<HourEvent> GetEvents(DateTime fromDate, DateTime toDate)
		{
			_oHourEvents = new List<HourEvent>();

			try
			{
				

				IDbConnection con = new SqlConnection(Configuration.GetConnectionString("MyCalendar"));
				if (con.State == ConnectionState.Closed) con.Open();

				string sql = string.Format(@"SELECT * FROM HourEvent WHERE HourEventDay BETWEEN '{0}' AND '{1}'", fromDate.ToString("dd-MMM-yyyy"), toDate.ToString("dd-MMM-yyyy"));

				var oHourEvents = con.Query<HourEvent>(sql).ToList();

				if (oHourEvents != null && oHourEvents.Count() > 0)
				{
					_oHourEvents = oHourEvents;
				}

				con.Close();

				

			} catch (Exception ex)
			{

			}

			return _oHourEvents;

		}

		public HourEvent SaveOrUpdate(HourEvent oHourEvent)
		{
			_oHourEvent = new HourEvent();
			try
			{
				int operationType = Convert.ToInt32(oHourEvent.HourEventId == 0 ? OperationType.Insert : OperationType.Update);

				IDbConnection con = new SqlConnection(Configuration.GetConnectionString("MyCalendar"));
				if (con.State == ConnectionState.Closed) con.Open();

				var oHourEvents = con.Query<HourEvent>("SP_HourEvent", this.SetParameters(oHourEvent, operationType), commandType: CommandType.StoredProcedure);

				if (oHourEvents != null && oHourEvents.Count() > 0)
				{
					_oHourEvent = oHourEvents.FirstOrDefault();
				}

				con.Close();
			}
			catch (Exception ex)
			{
				_oHourEvent.Message = ex.Message;
			}
			return _oHourEvent;
		}

	//	@Note varchar(MAX)

	//,@HourEventId int

	//,@HourEventHour varchar(10)

	//,@HourEventDay datetime

	//, @OperationType int

		private DynamicParameters SetParameters(HourEvent oHourEvent, int operationType)
		{
			DynamicParameters parameters = new DynamicParameters();

			parameters.Add("@Note", oHourEvent.Note);
			parameters.Add("@HourEventId", oHourEvent.HourEventId);
			parameters.Add("@HourEventHour", oHourEvent.HourEventHour);
			parameters.Add("@HourEventDay", oHourEvent.HourEventDay);
			parameters.Add("@OperationType", operationType);

			return parameters;
		}

		public List<HourEvent> GetEvents(DateTime eventDate)
		{
			_oHourEvents = new List<HourEvent>();

			try
			{


				IDbConnection con = new SqlConnection(Configuration.GetConnectionString("MyCalendar"));
				if (con.State == ConnectionState.Closed) con.Open();

				string sql = string.Format(@"SELECT * FROM HourEvent WHERE HourEventDay = '{0}'", eventDate.ToString("dd-MMM-yyyy"));

				var oHourEvents = con.Query<HourEvent>(sql).ToList();

				if (oHourEvents != null && oHourEvents.Count() > 0)
				{
					_oHourEvents = oHourEvents;
				}

				con.Close();



			}
			catch (Exception ex)
			{

			}

			return _oHourEvents;
		}
	}
}
