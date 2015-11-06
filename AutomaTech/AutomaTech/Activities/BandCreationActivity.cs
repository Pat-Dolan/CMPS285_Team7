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
	public class BandCreationActivity : Activity
	{
		GlobalVariables GBand = GlobalVariables.getInstance();
		EditText bandName;
		Button newBand;
		Button addMember;
		Button back;

		string conString = string.Format("Server=104.225.129.25;Database=f15-s1-t7;User Id=s1-team7;Password=!@QWaszx;Integrated Security=False");
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//Setting layout
			SetContentView(Resource.Layout.BandCreationLayout);

			bandName = FindViewById<EditText>(Resource.Id.txtNewBandName);

			addMember = FindViewById<Button> (Resource.Id.btnAddMember);
			addMember.Click += AddMember_Click;

			back = FindViewById<Button> (Resource.Id.btnBackBandNew);
			back.Click += Back_Click;

				newBand = FindViewById<Button> (Resource.Id.finishCreation);
			newBand.Click += NewBand_Click;
		}

		void Back_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(BandMainActivity));
		}

		void AddMember_Click (object sender, EventArgs e)
		{
			string result = "Add Member Here";
			Toast.MakeText (this, result, ToastLength.Short).Show ();
		}

		void NewBand_Click (object sender, EventArgs e)
		{
			using (SqlConnection connection = new SqlConnection(conString))
			{

				SqlCommand cmd = new SqlCommand("INSERT INTO bandList (bandId, bandName, bandManager, bandVisible) VALUES (@Id, @BandName, @Manager, @Visible)");
				cmd.CommandType = CommandType.Text;
				cmd.Connection = connection;
				cmd.Parameters.AddWithValue ("@Id", (GBand.getBandTotal() + 1));
				cmd.Parameters.AddWithValue ("@BandName", bandName.Text);
				cmd.Parameters.AddWithValue ("@Manager", "The Boss");
				cmd.Parameters.AddWithValue ("@Visible", 1);  
				connection.Open();
				cmd.ExecuteNonQuery();
				connection.Close ();
			}

			StartActivity (typeof(BandMainActivity));
		}
	}
}