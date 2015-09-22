using System;
using System.Data;
using System.IO;
using SQLite;

namespace AutomaTech
{
	[Table("UserInfo")]

	public class UserInfo
	{
		[PrimaryKey, AutoIncrement, Column("_Id")]
		public int ID{ get; set; }

		[MaxLength(20), Column("User_Name")]
		public string UserName{ get; set; }

		[MaxLength(20), Column("Password")]
		public string Pass{ get; set; }

		[MaxLength(20), Column("Profile_Name")]
		public string ProfileName{ get; set;}

		[Column("Access Level")]
		public int Access{ get; set; }

		[MaxLength(20), Column("First Name")]
		public string FirstName{get;set;}

		[MaxLength(20), Column("Last Name")]
		public string LastName{ get; set; }

		[MaxLength(30), Column("Email")]
		public string Email{ get; set; }

	}
}

