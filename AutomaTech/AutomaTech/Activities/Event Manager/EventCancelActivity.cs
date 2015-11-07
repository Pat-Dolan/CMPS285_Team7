
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
using System.Data;
using System.Data.SqlClient;

namespace AutomaTech
{
	[Activity (Label = "TourPlus+", Icon = "@drawable/Icon")]			
	public class EventCancelActivity : Activity
	{
		GlobalVariables GEventID = GlobalVariables.getInstance();
		Button eventCancel;
		Button back;
		string conString = string.Format("Server=104.225.129.25;Database=f15-s1-t7;User Id=s1-team7;Password=!@QWaszx;Integrated Security=False");
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.EventCancelLayout);
			eventCancel = FindViewById<Button> (Resource.Id.btnCancel);
			eventCancel.Click += EventCancel_Click;
			//TextView title = FindViewById<TextView> (Resource.Id.txtCancelTitle);

			back = FindViewById<Button> (Resource.Id.btnCancelBack);
			back.Click += Back_Click;

			//EventDB dbr = new EventDB ();
			//var item = dbr.GetEventById (GEventID.getEventId () + 1);


		}

		void Back_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(EventSelectActivity));
		}

		void EventCancel_Click (object sender, EventArgs e)
		{
			using (SqlConnection connection = new SqlConnection(conString))
			{

				SqlCommand cmd = new SqlCommand("UPDATE EventList SET visible = @Visible WHERE id = @Id ");
				cmd.CommandType = CommandType.Text;
				cmd.Connection = connection;

				cmd.Parameters.AddWithValue ("@Id", GEventID.getEventId());
				cmd.Parameters.AddWithValue ("@Visible", 0);

				connection.Open();
				cmd.ExecuteNonQuery();
				connection.Close ();
			}
			StartActivity (typeof(EventMainActivity));
		}


	}
}

