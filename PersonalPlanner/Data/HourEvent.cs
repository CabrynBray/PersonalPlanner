using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Calendar.Data
{
	public class HourEvent
	{

		/*
		 
		 Crear modelo: EventHour
		EventHour tiene:

		Nota - String
		FechaYHora - DateTime
		
		Obtener fecha y hora actuales

		Obtener 7 dias siguientes
		
		 */

		[BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
		public int HourEventId { get; set; }

		public DateTime HourEventDay { get; set; }

		public int HourEventHour {  get; set; }

		public string Note { get; set; }

		[BsonIgnore]
		public string Message { get; set; }
	}
}
