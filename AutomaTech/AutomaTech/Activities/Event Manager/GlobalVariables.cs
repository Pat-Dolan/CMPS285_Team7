using System;

namespace AutomaTech
{
	public class GlobalVariables
	{
		private static GlobalVariables instance;
		private static int eventId;
		private static int eventTotal;
		private static int bandId;
		private static int bandTotal;
		private static long userId;
		private static int defaultBandId;
		private static int memberTotal;
		private static long memberId;
		private static long managerId;
		private static int accessLevel;
		private static string userName;
		private static string memberName;
		private static int confirm;
		private static string bandName;
		private static int userTotal;

		public void setUserTotal(int total)
		{
			GlobalVariables.userTotal = total;
		}
		public int getUserTotal()
		{
			return GlobalVariables.userTotal;
		}

		public void setBandName(string name)
		{
			GlobalVariables.bandName = name;
		}
		public string getBandName()
		{
			return GlobalVariables.bandName;
		}

		public void setMemberName(string name)
		{
			GlobalVariables.memberName = name;
		}
		public string getMemberName()
		{
			return GlobalVariables.memberName;
		}


		public void setConfirm(int conf)
		{
			GlobalVariables.confirm = conf;
		}
		public int getConfirm()
		{
			return GlobalVariables.confirm;
		}

		public void setUserName(string name)
		{
			GlobalVariables.userName = name;
		}
		public string getUserName()
		{
			return GlobalVariables.userName;
		}

		public void setAccessLevel(int access)
		{
			GlobalVariables.accessLevel = access;
		}
		public int getAccessLevel()
		{
			return GlobalVariables.accessLevel;
		}

		public void setManagerId(long id)
		{
			GlobalVariables.managerId = id;
		}
		public long getManagerId()
		{
			return GlobalVariables.managerId;
		}
		public void setMemberId(long id)
		{
			GlobalVariables.memberId = id;
		}
		public long getMemberId()
		{
			return GlobalVariables.memberId;
		}
		public void setMemberTotal(int total)
		{
			GlobalVariables.memberTotal = total;
		}
		public int getMemberTotal()
		{
			return GlobalVariables.memberTotal;
		}

		public void setDefaultBandId(int id)
		{
			GlobalVariables.defaultBandId = id;
		}
		public int getDefaultBandId()
		{
			return GlobalVariables.defaultBandId;
		}

		public void setUserId(long id)
		{
			GlobalVariables.userId = id;
		}
		public long getUserId()
		{
			return GlobalVariables.userId;
		}

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

		public void setBandId(int id)
		{
			GlobalVariables.bandId = id;
		}

		public void setBandTotal(int total)
		{
			GlobalVariables.bandTotal = total;
		}

		public int getBandId()
		{
			return GlobalVariables.bandId;
		}

		public int getBandTotal()
		{
			return GlobalVariables.bandTotal;
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

