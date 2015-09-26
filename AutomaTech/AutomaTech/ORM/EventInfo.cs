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
		public string EventName{ get; set;}
		public string date{ get; set; }
		public string time{ get; set;}
	}
}

