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

		public int HourEventId { get; set; }

		public DateTime HourEventDay { get; set; }

		public int HourEventHour {  get; set; }

		public string Note { get; set; }

		public string Message { get; set; }
	}
}
