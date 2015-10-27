using System;
using SQLite;
using System.IO;


namespace AutomaTech
{
	public class EventDB
	{
		public string CreateDB()
		{
			var output = "";
			var tableOutput = "";
			output += "Creating Database if it doesn't exist.";
			string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "AutomatechORM.db3");

			var db = new SQLiteConnection (dbPath);
			output += "\nDatabase Created...";

			tableOutput = CreateEventTable ();
			output += " " + tableOutput;
			return output;
		}
		//Creating table
		public string CreateEventTable()
		{
			try
			{
				string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "AutomatechORM.db3");
				var db = new SQLiteConnection(dbPath);
				db.CreateTable<EventInfo>();
				string result = "Table created successfully...";
				return result;
			}
			catch(Exception ex)
			{
				return "Error : " + ex.Message;
			}
		}

		//Inserting record
		public string InsertEvent(string name, string location, string date, string time)
		{
			try
			{
				string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "AutomatechORM.db3");

				var db = new SQLiteConnection (dbPath);
				EventInfo item = new EventInfo();
				item.title = name;
				item.location = location;
				item.date = date;
				item.time = time;
				item.access = 1;
				db.Insert(item);
				return "Record added...";
			}
			catch(Exception ex) 
			{
				return "Error : " + ex.Message;
			}
		}

		//TESTING FUNCTION
		//This function returns all events as a toast
		public string GetEvent()
		{
			string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "AutomatechORM.db3");
			var db = new SQLiteConnection(dbPath);

			string output = "";
			output += "Retrieving the data using ORM...";
			var table = db.Table<EventInfo> ();

			foreach (var item in table) 
			{
				output += "\n"+ item.ID + " " + item.title + " " + item.location + "  " + item.date +"  " + item.time;
			}
			return output;
		}

		//NEEDS WORK
		//This function returns an event table based on id
		public EventInfo GetEventById(int id)
		{
			string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "AutomatechORM.db3");
			var db = new SQLiteConnection (dbPath);
			var table = db.Table<EventInfo> ();

			var item = db.Get<EventInfo> (id);

			return item;
		}

		//NEEDS INTEGRATION
		//This function updates an existing event with new info
		public string updateEvent(int id,string name, string location, string date, string time)
		{
			string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "AutomatechORM.db3");
			var db = new SQLiteConnection(dbPath);
			var item = db.Get<EventInfo>(id) ;
			item.title = name;
			item.location = location;
			item.date = date;
			item.time = time;
			db.Update (item);
			return "Record Updated...";
		}

		public int getEventTotal()
		{
			string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "AutomatechORM.db3");
			var db = new SQLiteConnection(dbPath);

			int count = 0;
			var table = db.Table<EventInfo> ();

			foreach (var item in table) 
			{
				count++;
			}
			return count;
		}


		//NEEDS INTERGRATION
		//This function removes an event based on id
		public string RemoveEvent(int id)
		{
			string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "AutomatechORM.db3");
			var db = new SQLiteConnection (dbPath);
			var item = db.Get<EventInfo> (id);
			item.access = 0;
			return "Record Deleted...";
		}
	}
}

