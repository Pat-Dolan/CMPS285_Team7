﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using System.Data;
using System.Data.SqlClient;

namespace AutomaTech
{
	[Activity (Label = "TourPlus+", Icon = "@drawable/Icon")]	
	public class MemberNewActivity : Activity 
	{
		GlobalVariables GMember = GlobalVariables.getInstance();
		Button addMember;
		Button back;
		EditText memberName;
		string conString = string.Format("Server=104.225.129.25;Database=f15-s1-t7;User Id=s1-team7;Password=!@QWaszx;Integrated Security=False");

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//Setting layout
			SetContentView (Resource.Layout.NewMemberLayout);

			addMember = FindViewById<Button> (Resource.Id.btnNewMember);
			addMember.Click += AddMember_Click;

			memberName = FindViewById<EditText> (Resource.Id.txtAddMember);
		

			back = FindViewById<Button> (Resource.Id.btnNewMemberBack);
			back.Click += Back_Click;

		}

		void Back_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(BandCreationActivity));
		}

		void AddMember_Click (object sender, EventArgs e)
		{
			bool memberFound = false;
			IDbConnection dbcon;
			using (dbcon = new SqlConnection (conString)) 
			{
				dbcon.Open ();
				using (IDbCommand dbcmd = dbcon.CreateCommand ()) 
				{
					string sqlGetTitle = " SELECT (userName), (access),(managerId), (visible), (defaultBandId)" +
						" FROM userList  ";
					dbcmd.CommandText = sqlGetTitle;
					using (IDataReader reader = dbcmd.ExecuteReader ()) 
					{
						//nCount = 0;
						while ((reader.Read ()) && (memberFound == false)) 
						{

							//long memberUserId = (long)reader ["userId"];
							string memName = (string)reader ["userName"];
							int access = (int)reader ["access"];
							long memberManager = (long)reader ["managerId"];
							int memberVisible = (int)reader ["visible"];
							int defaultBandId = (int)reader ["defaultBandId"];
							//int confirm = (int)reader ["confirmed"];
							memName = memName.Trim ();
							//Member determined by defaultBandId, determined by user if mutiple bands	
							if ((memName == memberName.Text) && (access == 0)) 
							{
								//add member to band if found
								using (SqlConnection connection = new SqlConnection (conString)) 
								{

									SqlCommand cmd = new SqlCommand ("UPDATE userList SET managerId = @ManagerId, defaultBandId = @BandId, visible = @Visible WHERE userName = @Name ");
										cmd.CommandType = CommandType.Text;
										cmd.Connection = connection;

										cmd.Parameters.AddWithValue ("@ManagerId", GMember.getUserId ());
										cmd.Parameters.AddWithValue ("@BandId", GMember.getBandId ());
										cmd.Parameters.AddWithValue ("@Name", memberName.Text);
										cmd.Parameters.AddWithValue ("@Visible", 1);

										connection.Open ();
										cmd.ExecuteNonQuery ();
										connection.Close ();
								}

								memberFound = true;
							}
						}

						reader.Close ();
						dbcon.Close ();
					}

					//Display search result
					if (memberFound == true) 
					{
						string result = memberName.Text + " was added";
						Toast.MakeText (this, result, ToastLength.Short).Show ();
					} 
					else 
					{
						//report that the search was unsuccessful
						string result = memberName.Text + " was not found";
						Toast.MakeText (this, result, ToastLength.Short).Show ();
					}
				}
			}

			StartActivity (typeof(NewMemberMainActivity));
		}
			
	}
}

