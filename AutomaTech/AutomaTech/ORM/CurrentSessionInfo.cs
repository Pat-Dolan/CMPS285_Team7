using System;

namespace AutomaTech
{

	//This class is intended to be used as a current session information saver. It may be useful
	public class CurrentSessionInfo
	{
		int Id { get; set;}
		string ProfileName {get;set;}
		string Username { get; set;}
		int Access { get; set;}

		public CurrentSessionInfo (int id, string profileName, string username, int access)
		{
			Id = id;
			ProfileName = profileName;
			Username = username;
			Access = access;
		}
	}
}

