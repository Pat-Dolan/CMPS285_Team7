using System;

namespace AutomaTech
{
	public class Event
	{
		public string eventTitle{ get; set; }
		public string eventDate{ get; set; }
		public int eventId{ get; set; }
		public int eventVisible{ get; set; }

		public Event (int Id, string title, string date, int visible)
		{
			eventId = Id;
			eventTitle = title;
			eventDate = date;
			eventVisible = visible;
		}
	}
}

