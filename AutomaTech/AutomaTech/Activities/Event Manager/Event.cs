using System;

namespace AutomaTech
{
	public class Event
	{

		public string eventTitle{ get; set; }
		public string eventDate{ get; set; }

		public Event (string title, string date)
		{
			eventTitle = title;
			eventDate = date;
		}
	}
}

