using System;

namespace AutomaTech
{
	//This class is used to hold facebook information and to connect individual users to their information in the database
	public class User
	{
		public long userId{ get; set; }
		public string userName{ get; set;}
		public long userManager{ get; set;}
		public int userAccess{get;set;}
		public int userDefaultBand{get;set;}
		public int userConfirm{get;set;}
		public User (long id, string name)
		{
			userId = id;
			userName = name;
		}

		//Initial login id
		public User(long id)
		{
			userId = id;
		}
	}
}

