using System;

namespace AutomaTech
{
	
	public class GlobalVariables
	{
		private static GlobalVariables instance;
		private static int eventId;
	
		public void setEventId(int t)
		{
			GlobalVariables.eventId = t;
		}
		public int getEventId()
		{
			return GlobalVariables.eventId;
		}

		public static GlobalVariables getInstance()
		{
			if (instance == null) {
				instance = new GlobalVariables ();
			}
			return instance;
		}
	
	}
}

