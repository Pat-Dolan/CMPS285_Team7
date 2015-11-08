using System;

namespace AutomaTech
{
	public class Band
	{
		public int bandId{ get; set;}
		public long bandMgrId{ get; set; }
		public string bandName{ get; set; }
		public int bandVisible{ get; set;}
		public Band (int ID, string name, long manager, int visible)
		{
			bandId = ID;
			bandName = name;
			bandMgrId = manager;
			bandVisible = visible;
		}
	}
}

