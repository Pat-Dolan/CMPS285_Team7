using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AutomaTech
{
	[Activity (Label = "TourPlus+", Icon = "@drawable/Icon")]
	public class EventMainActivity : Activity
	{

		GlobalVariables GEventID = GlobalVariables.getInstance();	//Should be used to get username from Facebook
		int nCount = 0;
		private List<Event> nEvents;	
		private ListView nEventListView;


		string conString = string.Format("Server=104.225.129.25;Database=f15-s1-t7;User Id=s1-team7;Password=!@QWaszx;Integrated Security=False");

		Button firstEventbtn;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.EventMainLayout);

			firstEventbtn = FindViewById<Button> (Resource.Id.btnFirstEvent);
			firstEventbtn.Click += FirstEventbtn_Click;

			Button back = FindViewById<Button> (Resource.Id.btnEventMainBack);
			back.Click += Back_Click;

			nEventListView = FindViewById<ListView> (Resource.Id.lvEvent);

			nEvents = new List<Event>();

			//SQLite Database
			//			EventDB dbr = new EventDB ();
			//			var result = dbr.CreateDB ();
			//			nCount = dbr.getEventTotal ();


			//			Building the event table
			//			if (nCount != 0) {
			//				for (int i = 1; i <= nCount; i++) {
			//					var nextEvent = dbr.GetEventById (i);
			//					nEvents.Add (new Event (nextEvent.title, nextEvent.date));
			//				}
			//			}

			//SQL Server Database
			UpdateEventList ();


			nEventListView.ItemClick += NEventListView_ItemClick;
		}
		void UpdateEventList ()
		{
			//getting titles for initial event list view
			IDbConnection dbcon;
			using (dbcon = new SqlConnection (conString)) 
			{
				dbcon.Open ();
				using (IDbCommand dbcmd = dbcon.CreateCommand ()) 
				{
					string sqlGetTitle = " SELECT (id),(title),(date),(time), (visible) " +
						" FROM eventinfo ";
					dbcmd.CommandText = sqlGetTitle;
					using (IDataReader reader = dbcmd.ExecuteReader ()) 
					{
						nCount = 0;
						while (reader.Read ()) 
						{

							int eventId = (int)reader ["id"];
							string eventTitle = (string)reader ["title"];
							string eventDate = (string)reader ["date"];
							int eventVisible = (int)reader ["visible"];
							//for cancel	
							if (eventVisible == 1) {
								nEvents.Add (new Event ((eventId), eventTitle, eventDate, eventVisible));
							}

							
							nCount++;
						}
						GEventID.setEventTotal (nCount);
						reader.Close ();
						dbcon.Close ();
					}
				}
			}
			EventListViewAdapter adapter = new EventListViewAdapter (this, nEvents);

			nEventListView.Adapter = adapter;
		}
		void Back_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(MainActivity));
		}



		void NEventListView_ItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			GEventID.setEventId(nEvents[e.Position].eventId);
			string result = " " + nEvents[e.Position].eventId;
			Toast.MakeText (this, result, ToastLength.Short).Show ();
			StartActivity (typeof(EventSelectActivity));
		}

		void FirstEventbtn_Click (object sender, EventArgs e)
		{
			StartActivity (typeof(EventNewActivity));
		}
	}
}



