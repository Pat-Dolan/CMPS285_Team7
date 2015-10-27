using System;
using System.Data;
using System.IO;
using SQLite;

namespace AutomaTech
{
	[Table("EventInfo")]
	public class EventInfo
	{
		[PrimaryKey, AutoIncrement, Column("_Id")]
		public int ID{ get; set; }
		public string location{ get; set;}
		public string title{ get; set;}
		public string date{ get; set; }
		public string time{ get; set;}
		public int access{ get; set; }	//visibility 1 for visible, 0 for invisible

	}
}


