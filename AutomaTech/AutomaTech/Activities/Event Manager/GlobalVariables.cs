using System;

namespace AutomaTech
{
	
	public class GlobalVariables
	{
		private static GlobalVariables instance;
		private static int eventId;
		private static int eventTotal;
	
		public void setEventId(int t)
		{
			GlobalVariables.eventId = t;
		}
		public int getEventId()
		{
			return GlobalVariables.eventId;
		}
		public void setEventTotal(int total)
		{
			GlobalVariables.eventTotal = total;
		}
		public int getEventTotal()
		{
			return GlobalVariables.eventTotal;
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

