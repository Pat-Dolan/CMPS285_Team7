
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Data;
using System.Data.SqlClient;

namespace AutomaTech
{
	[Activity (Label = "TourPlus+", Icon = "@drawable/Icon")]			
	public class EventViewActivity : Activity

	{
		GlobalVariables GEventID = GlobalVariables.getInstance();
		private TextView nTitle;
		private TextView nLocation;
		private TextView nDate;
		private TextView nTime;
		private Button nBack;
		string conString = string.Format("Server=104.225.129.25;Database=f15-s1-t7;User Id=s1-team7;Password=!@QWaszx;Integrated Security=False");
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView(Resource.Layout.EventViewLayout);

			nTitle = FindViewById<TextView> (Resource.Id.txtTitle);
			nLocation = FindViewById<TextView> (Resource.Id.txtLocation);
			nDate = FindViewById<TextView> (Resource.Id.txtDate);
			nTime = FindViewById<TextView> (Resource.Id.txtTime);

			nBack = FindViewById<Button> (Resource.Id.btnViewBack);
			nBack.Click += NBack_Click;

			SetFields (GEventID.getEventId());

		}

		void SetFields(int ID)
		{

			//SQLite Database
			//			EventDB dbr = new EventDB ();
			//			var selectedEvent = dbr.GetEventById ((GEventID.getEventId ()));
			//				nTitle.Text = selectedEvent.title;
			//				nLocation.Text = selectedEvent.location;
			//				nDate.Text = selectedEvent.date;
			//				nTime.Text = selectedEvent.time;

			bool found = false;
			int eventId;
			IDbConnection dbcon;
			using (dbcon = new SqlConnection (conString)) 
			{
				dbcon.Open ();
				using (IDbCommand dbcmd = dbcon.CreateCommand ()) 
				{
					string sqlGetEventInfo = " SELECT (id), (title), (location), (date), (time) FROM eventinfo ";

					dbcmd.CommandText = sqlGetEventInfo;
					using (IDataReader reader = dbcmd.ExecuteReader ()) 
					{
						while (reader.Read() && (found == false)) 
						{
							eventId = (int)reader ["id"];

							nTitle.Text = (string)reader ["title"];
							nLocation.Text = (string)reader ["location"];
							nDate.Text = (string)reader ["date"];
							nTime.Text = (string)reader ["time"];
							if(eventId == ID)
								found = true;
						}
						reader.Close ();
						dbcon.Close ();
					}
				}
			}
		}

		void NBack_Click (object sender, EventArgs e)
		{
			StartActivity(typeof(EventSelectActivity));
		}


	}
}

