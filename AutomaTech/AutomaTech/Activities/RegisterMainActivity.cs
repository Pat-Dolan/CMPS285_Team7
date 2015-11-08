using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Data.SqlClient;
using System.Data;

namespace AutomaTech
{
	[Activity (Label = "TourPlus+", Icon = "@drawable/Icon")]			
	public class RegisterMainActivity : Activity
	{
		
		GlobalVariables GUser = GlobalVariables.getInstance();
		string conString = string.Format("Server=104.225.129.25;Database=f15-s1-t7;User Id=s1-team7;Password=!@QWaszx;Integrated Security=False");

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//Setting layout
			SetContentView(Resource.Layout.RegisterMainLayout);

			//Radio group components
			RadioButton accessM = FindViewById<RadioButton> (Resource.Id.rBtnManager);
			RadioButton accessA = FindViewById<RadioButton> (Resource.Id.rBtnArtist);

			accessM.Click += AccessM_Click;
			accessA.Click += AccessA_Click;
		
			//Begins registration check
			Button register = FindViewById<Button>(Resource.Id.btnRegister);
			register.Click += Register_Click;

		}
			
		//This handler sets access level for Artists
		void AccessA_Click (object sender, EventArgs e)
		{
			GUser.setAccessLevel (0);
			GUser.setManagerId (0);
		}

		//This handler sets access level for Managers
		void AccessM_Click (object sender, EventArgs e)
		{
			GUser.setAccessLevel (1);
			GUser.setManagerId (GUser.getUserId ());
		}

		//This handler tests and saves information for a new user
		void Register_Click (object sender, EventArgs e)
		{
			using (SqlConnection connection = new SqlConnection(conString))
			{

				SqlCommand cmd = new SqlCommand("INSERT INTO userList (id, userId, userName, managerId, visible, access, defaultBandId, confirmed ) VALUES (@Id, @UserId, @UserName, @ManagerId, @Visible,@Access, @Default, @Confirm)");
				cmd.CommandType = CommandType.Text;
				cmd.Connection = connection;

				cmd.Parameters.AddWithValue ("@Id", (GUser.getUserTotal() + 1));
				cmd.Parameters.AddWithValue ("@UserId", GUser.getUserId ());
				cmd.Parameters.AddWithValue ("@UserName", ("User" +	(GUser.getUserTotal() + 1)));		//GUser.getUserName()); from FACEBOOK
				cmd.Parameters.AddWithValue ("@ManagerId", GUser.getManagerId());
				cmd.Parameters.AddWithValue ("@Visible", 1);
				cmd.Parameters.AddWithValue ("@Access", GUser.getAccessLevel());
				cmd.Parameters.AddWithValue ("@Default", GUser.getDefaultBandId ());
				cmd.Parameters.AddWithValue ("@Confirm", 1);

				connection.Open();
				cmd.ExecuteNonQuery();
				connection.Close ();
			}

			StartActivity (typeof(MainActivity));
		}
	}
}

