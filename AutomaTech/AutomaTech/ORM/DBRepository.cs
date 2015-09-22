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

			InsertRecord (user, pass, profile, access, firstName, lastName, email);

			return output;
		}
		//Creating table
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



		//Inserting record
		public string InsertRecord(string user, string pass, string profile, int access, string firstName, string lastName, string email)
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
			

		//Code to retrieve records
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
		//code to retrieve record using ORM
		public UserInfo GetTableById(int id)
		{
			string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "AutomatechORM.db3");
			var db = new SQLiteConnection (dbPath);
			var table = db.Table<UserInfo> ();

			var item = db.Get<UserInfo> (id);

			return item;
		}

		//Update using ORM
		public string updateRecord(int id, string task)
		{
			string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "AutomatechORM.db3");
			var db = new SQLiteConnection(dbPath);
			var item = db.Get<UserInfo> (id);
			item.UserName = task;
			db.Update (item);
			return "Record Updated...";
		}

		//Removing record
		public string RemoveTask(int id)
		{
			string dbPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "AutomatechORM.db3");
			var db = new SQLiteConnection (dbPath);
			var item = db.Get<UserInfo> (id);
			db.Delete (item);
			return "Record Deleted...";
		}
			

	}
}

