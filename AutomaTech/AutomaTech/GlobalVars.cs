using System;

//This class is for storing login info and user state for access to info from activity to activity
namespace AutomaTech
{
	public class GlobalVars
	{
		private static GlobalVars instance;
		private static int Test;
		private GlobalVars(){}

		public void setTest(int t)
		{
			GlobalVars.Test = t;
		}
		public int getTest()
		{
			return GlobalVars.Test;
		}

		public static GlobalVars getInstance()
		{
			if (instance == null) {
				instance = new GlobalVars ();
			}
			return instance;
		}
	}
}

///IT WORKS!!! ADD STRING FOR PROFILE NAME AND INT OR BOOL FOR ACCESS LEVEL THEN ASSIGN IN LOGIN BASED ON TABLE BAMPH!!

