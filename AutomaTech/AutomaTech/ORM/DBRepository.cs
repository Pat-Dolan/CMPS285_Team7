using System;
using System.Data;
using System.IO;
using SQLite;

namespace AutomaTech
{
	public class DBRepository
	{
		//Creating Database
		public string CreateDB(string user, string pass, string profile, int access, string firstName, string lastName, string email)
		{
			var output = "";
			output += "Creating Database if it doesn't exist.";
			string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "AutomatechORM.db3");

			var db = new SQLiteConnection (dbPath);
			output += "\nDatabase Created...";

			CreateTable ();

			InsertUser (user, pass, profile, access, firstName, lastName, email);

			return output;
		}

		//Overloading the createdb function for first time logins
		public string CreateDB()
		{
			var output = "";
			output += "Creating Database if it doesn't exist.";
			string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "AutomatechORM.db3");

			var db = new SQLiteConnection (dbPath);
			output += "\nDatabase Created...";

			CreateTable ();
			return output;
		}

		//This function creates the table
		public string CreateTable()
		{
			try
			{
				string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "AutomatechORM.db3");
				var db = new SQLiteConnection(dbPath);
				db.CreateTable<UserInfo>();
				string result = "Table created successfully...";
				return result;
			}
			catch(Exception ex)
			{
					return "Error : " + ex.Message;
			}
		}
			
		//This function inserts a new user
		public string InsertUser(string user, string pass, string profile, int access, string firstName, string lastName, string email)
		{
			try
			{
				string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "AutomatechORM.db3");

				var db = new SQLiteConnection (dbPath);
				UserInfo item = new UserInfo();
				item.UserName = user;
				item.Pass = pass;
				item.ProfileName = profile;
				item.Access = access;
				item.FirstName = firstName;
				item.LastName = lastName;
				item.Email = email;
				db.Insert(item);
				return "Record added...";
			}
			catch(Exception ex) 
			{
				return "Error : " + ex.Message;	
			}
		}
			
		//TEST FUNCTION
		//This function returns all records as a Toast
		public string GetAccount()
		{
			string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "AutomatechORM.db3");
			var db = new SQLiteConnection(dbPath);

			string output = "";
			output += "Retrieving the data using ORM...";
			var table = db.Table<UserInfo> ();

			foreach (var item in table) 
			{
				output += "\n" + item.ID + " " + item.UserName + "  " + item.ProfileName +"  " + item.Access;
			}
			return output;
		}

		//This function returns the user table by specific iod
		public UserInfo GetUserById(int id)
		{
			string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "AutomatechORM.db3");
			var db = new SQLiteConnection (dbPath);
			var table = db.Table<UserInfo> ();

			var item = db.Get<UserInfo> (id);

			return item;
		}

		//TESTING FUNCTION TO DISPLAY USERNAME BY ID
		public string DisplayUserById(int id)
		{
			string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "AutomatechORM.db3");
			var db = new SQLiteConnection (dbPath);
			var table = db.Table<UserInfo> ();

			string username = "";

			var item = db.Get<UserInfo> (id);
			username = item.UserName.ToString ();
			return username;
		}

		//This function tests login information and returns the id index if found, and -1 if not found
		public int GetUserByLogin(string name, string pass)
		{
			string testName;
			string testPass;
			string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "AutomatechORM.db3");
			var db = new SQLiteConnection (dbPath);
			var table = db.Table<UserInfo> ();
			int index = -1;

			foreach(var item in table)
			{		testName = item.UserName.ToString();
					testPass = item.Pass.ToString();
				if((name == testName) && (pass == testPass))
					index = item.ID;
			}

			return index;
		}

		//This functio tests to see if a new username exists already
		public bool TestUsername(string newName)
		{
			int test = -1;
			bool valid = true;

			string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "AutomatechORM.db3");
			var db = new SQLiteConnection (dbPath);
			var table = db.Table<UserInfo> ();
			foreach (var item in table) 
			{
				if (newName == item.UserName)
					test++;
			}
			if (test != -1)
				valid = false;
			else
				valid = true;
			return valid;
		}

		//NEEDS INTEGRATION
		//This function will update user information
		public string updateUser(int id, string info)
		{
			string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "AutomatechORM.db3");
			var db = new SQLiteConnection(dbPath);
			var item = db.Get<UserInfo> (id);
			item.UserName = info;
			db.Update (item);
			return "Record Updated...";
		}

		//NEEDS INTEGRATION
		//This function removes a user by id
		public string RemoveUser(int id)
		{
			string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "AutomatechORM.db3");
			var db = new SQLiteConnection (dbPath);
			var item = db.Get<UserInfo> (id);
			db.Delete (item);
			return "Record Deleted...";
		}


		//Events

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
		/*	
		//Inserting record
		public string InsertEvent(string name, string location, string date, string time)
		{
			try
			{
				string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "AutomatechORM.db3");

				var db = new SQLiteConnection (dbPath);
				EventInfo item = new EventInfo();
				item.EventName = name;
				item.location = location;
				item.date = date;
				item.time = time;
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
				output += "\n" + item.EventName + " " + item.location + "  " + item.date +"  " + item.time;
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
			item.EventName = name;
			item.location = location;
			item.date = date;
			item.time = time;
			db.Update (item);
			return "Record Updated...";
		}

		//NEEDS INTERGRATION
		//This function removes an event based on id
		public string RemoveEvent(int id)
		{
			string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "AutomatechORM.db3");
			var db = new SQLiteConnection (dbPath);
			var item = db.Get<EventInfo> (id);
			db.Delete (item);
			return "Record Deleted...";
		}
		*/
	}
}

